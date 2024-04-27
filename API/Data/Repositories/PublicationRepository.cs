using AutoMapper;
using AutoMapper.QueryableExtensions;
using JPS.Common;
using JPS.Data;
using Microsoft.EntityFrameworkCore;
using PublishWell.API.Common.Helper;
using PublishWell.API.Controllers.Publications.DTO;
using PublishWell.API.Data.Entities;
using PublishWell.API.Data.Enums;
using PublishWell.API.Extension;
using PublishWell.API.Interfaces;

namespace PublishWell.API.Data.Repositories
{
    /// <summary>
    /// Publication respository to handle publications related db operations.
    /// </summary>
    public class PublicationRepository : IPublicationsRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _http;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="http"></param>
        public PublicationRepository(DataContext context, IMapper mapper, IHttpContextAccessor http)
        {
            _context = context;
            _mapper = mapper;
            _http = http;
        }
        /// <summary>
        /// Adding new publications into db table.
        /// </summary>
        /// <param name="pub">data transfar object of publication</param>
        /// <returns>Publications DTO object.</returns>
        public async Task AddAsync(PublicationDTO pub)
        {
            var categorie = await _context.Categories
            .Where(c=> c.Id == pub.CategoryID)
            .SingleOrDefaultAsync();
            var publication = _mapper.Map<Publication>(pub);
            publication.PublicationDate = DateTime.UtcNow;
            publication.LastUpdatedDate = DateTime.UtcNow;
            publication.PublicationCategory = new PublicationCategorie{
                Categorie  = categorie,
                CategorieId = categorie.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                CreatedBy = _http.HttpContext.User.getUserName(),
                UpdatedAt = DateTime.Now,
                UpdatedBy =  _http.HttpContext.User.getUserName(),
                Publication = publication,
            };
            publication.PublicationAuthorId = _http.HttpContext.User.getID();
            publication.PublicationAuthor =  await _context.Users
            .Where(u=> u.Id == _http.HttpContext.User.getID()).SingleOrDefaultAsync();
            var tags = pub.Tags.Split(',');
            var tagsToInsert = new List<PublicationTag>();
            foreach(string tag in tags)
            {
                tagsToInsert.Add( new PublicationTag{
                Text = tag,
                Publication = publication
            });
            }
            publication.PublicationTags = tagsToInsert;
            await _context.Publications.AddAsync(publication);
            // var p = await _context.Publications.FindAsync(publication);
            
        }

        /// <summary>
        /// Delete publication by id from db table.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A task that completes when the publication is deleted.</returns>
        public async Task DeleteAsync(int id)
        {
            var publication = await _context.Publications.FindAsync(id);
            publication.Status = Enums.PublicationStatus.Deleted;
            _context.Entry(publication).State = EntityState.Modified;

        }
        /// <summary>
        /// Get all publications from db table.
        /// </summary>
        /// <returns>Publications DTO object.</returns>
        public async Task<IEnumerable<PublicationDTO>> GetAll()
        {
            var publications = await _context.Publications.Where(p => p.Status != Enums.PublicationStatus.Deleted || p.Status != Enums.PublicationStatus.Draft)
                .OrderByDescending(p => p.PublicationDate)
                .Take(10)
                .ProjectTo<PublicationDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return publications ?? new List<PublicationDTO>();
        }

        /// <summary>
        /// Get all publications from db table by filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Publications DTO object.</returns>
        public async Task<PagedList<PublicationDTO>> GetAllByFilter(PublicationsFilter filter)
        {
            var query = _context.Publications.AsQueryable();
            // Removing the Draft and deleted publications
            query = query.Where(p => p.Status != PublicationStatus.Deleted && p.Status != PublicationStatus.Draft);

            // Finter By status 
            if (filter.Status != null)
            {
                query = query.Where(p => p.Status == filter.Status);
            }

            // filter by categories
            if (Utilities.IsNotNullOrEmpty(filter.PublicationCategorieName))
            {
                // query = query.Where(p => p.PublicationCategory?.Categorie?.Name == filter.PublicationCategorieName ?? false);
            }

            // Filter by start date and end date
            if (filter.StartDate != null && filter.EndDate != null)
            {
                query = query.Where(p => p.PublicationDate >= filter.StartDate && p.PublicationDate <= filter.EndDate);
            }

            // Filter by author id 
            // if(filter.AuthorID > 0)
            {
                query = query.Where(p => p.PublicationAuthor.Id == filter.AuthorID);
            }

            // filter by auther name
            if (Utilities.IsNotNullOrEmpty(filter.AuthorName))
            {
                query = query.Where(p => p.PublicationAuthor.UserName.ToLower() == filter.AuthorName.ToLower());
                //p.PublicationAuthor.FirstName.Contains(filter.AuthorName) || p.PublicationAuthor.LastName.Contains(filter.AuthorName));
            }

            //TODO: Add most Popular and least popular filters
            query = filter.FilterBy switch
            {
                FilterByEnum.Newest => query.OrderByDescending(c => c.Created),
                FilterByEnum.Oldest => query.OrderBy(c => c.Created),
                _ => query.OrderByDescending(c => c.Created)
            };

            return await PagedList<PublicationDTO>.CreateAsync(
            query.AsNoTracking()
            .ProjectTo<PublicationDTO>(_mapper.ConfigurationProvider),
            filter.PageNumber,
            filter.PageSize);
        }
        /// <summary>
        /// Get publication by id from db table.
        /// </summary>
        /// <param name="publicationId"></param>
        /// <param name="viewerID"></param>
        /// <returns>Publication object.</returns>
        public async Task<Publication> GetById(int publicationId, int viewerID)
        {
            if (publicationId <= 0) throw new ArgumentException("Invalid publication id");
            var publication = await _context.Publications
            .Include(p => p.PublicationComments)
            .Include(p => p.PublicationLikes)
            .Include(p => p.PublicationCategory)
            .Include(p => p.PublicationTags)
            .Where(u => u.Id == publicationId && u.Status != PublicationStatus.Deleted)
            .FirstOrDefaultAsync();

            if (publication.PublicationAuthorId != viewerID)
                await UpdateView(publication, viewerID);
            //Update the view count
            return (publication != null) ?
                    publication :
                    throw new Exception($"Publication with id {publicationId} not found");

        }

        /// <summary>
        /// Update publication by id from db table.
        /// </summary>
        /// <param name="publication"></param>
        public async Task UpdateAsync(Publication publication)
        {
            var publicationToUpdate = await _context.Publications.FindAsync(publication.Id);
            if (publicationToUpdate != null)
            {
                publicationToUpdate.EditCount += 1;
                publication.LastUpdatedDate = DateTime.UtcNow;
                if (publication.Status == PublicationStatus.Active)
                    publication.PublicationDate = DateTime.Now;
                _mapper.Map(publication, publicationToUpdate);
                //Update the edit count
                _context.Entry(publicationToUpdate).State = EntityState.Modified;
            }
            else
            {
                throw new Exception($"Publication with id {publication.Id} not found");
            }
        }

        // ADD Like comment view db changes
        private async Task UpdateView(Publication publication, int ViewerId)
        {
            var viewer = await _context.Users.Where(u => u.Id == ViewerId).SingleOrDefaultAsync();
            PublicationView view = new PublicationView
            {
                Publication = publication,
                PublicationId = publication.Id,
                Viewer = viewer
            };
            publication.PublicationViews.Add(view);
            await UpdateAsync(publication);
        }

        /// <summary>
        /// Add like to publication by id from db table.
        /// </summary>
        /// <param name="publicationId"></param>
        /// <param name="viewerId"></param>
        /// <returns>Returns if the task complete or not </returns>
        public async Task AddLikeAsync(int publicationId, int viewerId)
        {
            var publication = await _context.Publications.FindAsync(publicationId);
            var viewer = await _context.Users.FindAsync(viewerId);
            var like = new PublicationLike
            {
                AppUserId = viewerId,
                PublicationId = publicationId,
                Publication = publication,
                User = viewer,
                LikedAt = DateTime.Now
            };
            publication.PublicationLikes.Add(like);
            await UpdateAsync(publication);
        }

        /// <summary>
        /// Add comments in publications
        /// </summary>
        /// <param name="publicationId"></param>
        /// <param name="viewerId"></param>
        /// <param name="comment"></param>
        /// <returns>Task if it is complete or not</returns>
        public async Task AddCommentAsync(int publicationId, int viewerId, string comment)
        {
            var publication = await _context.Publications.Where(p => p.Id == publicationId).SingleOrDefaultAsync();
            var viewer = await _context.Users.Where(u => u.Id == viewerId).SingleOrDefaultAsync();

            var pubComment = new PublicationComment
            {
                Comment = comment,
                Created = DateTime.Now,
                Publication = publication,
                User = viewer,
                PublicationID = publicationId,
                UserID = viewerId
            };
            publication.PublicationComments.Add(pubComment);
            await UpdateAsync(publication);
        }

        /// <summary>
        /// Getting comments using filters 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>List of Publication Comments</returns>
        public async Task<PagedList<PublicationCommentDTO>> GetCommentsAsync(PublicationCommentFilter filter)
        {
            var query = _context.Publications.Where(p => p.Id == filter.PublicationID)
                .SelectMany(p => p.PublicationComments).AsQueryable();

            //TODO: Add most Popular and least popular filters
            query = filter.FilterBy switch
            {
                FilterByEnum.Newest => query.OrderByDescending(c => c.Created),
                FilterByEnum.Oldest => query.OrderBy(c => c.Created),
                _ => query.OrderByDescending(c => c.Created)
            };

            return await PagedList<PublicationCommentDTO>.CreateAsync(
            query.AsNoTracking()
            .ProjectTo<PublicationCommentDTO>(_mapper.ConfigurationProvider),
            filter.PageNumber,
            filter.PageSize);
        }

        /// <summary>
        /// Delete comment by id from db table.
        /// </summary>
        /// <param name="publicationId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public Task DeleteCommentAsync(int publicationId, int commentId)
        {
            var publication = _context.Publications.Find(publicationId);
            var comment = publication.PublicationComments.Where(c => c.Id == commentId).SingleOrDefault();
            if (comment == null)
            {
                throw new Exception($"Comment with id {commentId} not found");
            }
            publication.PublicationComments.Remove(comment);
            return UpdateAsync(publication);
        }
        /// <summary>
        /// Add categories for publications
        /// </summary>
        /// <param name="dto"> Categoriie dto</param>
        /// <returns>Created categorie</returns>
        public async Task AddCategorie(CategorieDTO dto)
        {
            if (dto == null) return;

            var categorie = _mapper.Map<Categorie>(dto);

            await _context.Categories.AddAsync(categorie);
        }

        /// <summary>
        /// Gets a Categorie by its ID.
        /// 
        /// This method retrieves a single Categorie object from the data store based on its ID.
        /// It uses LINQ to filter the Categories table by the provided ID and then projects the result
        /// to a CategorieDTO object using AutoMapper. If a Categorie is found, it returns the DTO;
        /// otherwise, it returns null.
        /// </summary>
        /// <param name="id">The ID of the Categorie to retrieve.</param>
        /// <returns>
        ///   A Task that resolves to a CategorieDTO object representing the retrieved Categorie, 
        ///   or null if not found.
        /// </returns>
        public async Task<CategorieDTO> GetCategorieByID(int id)
        {
            return await _context.Categories.Where(c => c.Id == id)
                        .ProjectTo<CategorieDTO>(_mapper.ConfigurationProvider)
                        .SingleOrDefaultAsync();
        }

        /// <summary>
        /// Deletes a Categorie from the data store.
        /// 
        /// This method first retrieves the Categorie object from the data store using its ID.
        /// If found, it removes the Categorie entity from the context's Categories collection.
        /// The actual deletion happens when the context's changes are saved (not shown here).
        /// </summary>
        /// <param name="id">The ID of the Categorie to delete.</param>
        /// <returns>
        ///   An asynchronous Task representing the deletion operation.
        /// </returns>
        public async Task DeleteCategorieAsync(int id)
        {
            var categorie = await _context.Categories.Where(c => c.Id == id).SingleOrDefaultAsync();
            _context.Categories.Remove(categorie);
        }

        /// <summary>
        /// Updates an existing Categorie in the data store.
        /// 
        /// This method uses AutoMapper to map the provided CategorieDTO object to a Categorie entity.
        /// It then sets the entity state of the mapped Categorie to Modified, indicating an update.
        /// The actual update happens when the context's changes are saved (not shown here).
        /// </summary>
        /// <param name="categorie">The CategorieDTO object containing the updated information.</param>
        /// <returns>
        ///   An already completed Task, signaling that the update preparation is done.
        /// </returns>
        public Task UpdateCategorieAsync(CategorieDTO categorie)
        {
            Categorie toUpdate = _mapper.Map<Categorie>(categorie);
            _context.Entry(toUpdate).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets all Categorie objects from the data store.
        /// 
        /// This method retrieves all Categorie entities from the Categories table.
        /// It uses AutoMapper to project the retrieved entities to a collection of CategorieDTO objects.
        /// </summary>
        /// <returns>
        ///   An asynchronous Task that resolves to an IEnumerable CategorieDTO collection
        ///   containing all CategorieDTO objects.
        /// </returns>
        public async Task<IEnumerable<CategorieDTO>> GetAllCategorie()
        {
            return await _context.Categories
                        .ProjectTo<CategorieDTO>(_mapper.ConfigurationProvider)
                        .ToListAsync();
        }


        /// <summary>
        /// Checks if a Categorie exists in the data store based on its ID.
        /// 
        /// This method uses LINQ to check if any Categorie entity exists in the Categories table
        /// where the ID matches the provided value. It returns true if a Categorie is found,
        /// otherwise false.
        /// </summary>
        /// <param name="id">The ID of the Categorie to check for existence.</param>
        /// <returns>
        ///   An asynchronous Task that resolves to a boolean value indicating whether a Categorie exists.
        /// </returns>
        public async Task<bool> IsCategorieExists(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}