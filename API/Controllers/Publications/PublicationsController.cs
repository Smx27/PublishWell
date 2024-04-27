using AutoMapper;
using JPS.Common;
using JPS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PublishWell.API.Common.Helper;
using PublishWell.API.Controllers.Publications.DTO;
using PublishWell.API.Data.Entities;
using PublishWell.API.Extension;
using PublishWell.API.Interfaces;

namespace PublishWell.API.Controllers.Publications;

/// <summary>
/// Publications controller that handles publication-related requests.
/// </summary>
[Authorize]
public class PublicationsController : BaseAPIController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly AppInfo _info;

    /// <summary>
    /// Initializes a new instance of the PublicationsController class.
    /// </summary>
    /// <param name="uow">The IUnitOfWork interface.</param>
    /// <param name="mapper">The IMapper interface.</param>
    /// <param name="info">The application info class to provide deployment data</param>
    public PublicationsController(IUnitOfWork uow, IMapper mapper, IOptions<AppInfo> info)
    {
        _uow = uow;
        _mapper = mapper;
        _info = info.Value;
    }
    /// <summary>
    /// Gets a Publication by its ID.
    /// </summary>
    /// <param name="id">The ID of the publication to retrieve.</param>
    /// <returns>An IActionResult object containing the retrieved PublicationDTO or an error message.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/publications/1
    ///
    /// </remarks>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPublicationByID(int id)
    {
        if (id <= 0) return BadRequest("Bad PublicationID");

        var publication = await _uow.publicationsRepository.GetById(id, User.getID());

        if (publication == null) return NotFound("Publication not found");

        return Ok(_mapper.Map<PublicationDTO>(publication));
    }

    /// <summary>
    /// Creates a new Publication.
    /// </summary>
    /// <param name="publication">The PublicationDTO object representing the new publication.</param>
    /// <returns>An IActionResult object containing the newly created PublicationDTO or an error message.</returns>
    [HttpPost]
    public async Task<IActionResult> CreatePublication(PublicationDTO publication)
    {
        if (!ModelState.IsValid) return BadRequest("One or mor evalidation Failed");

        await _uow.publicationsRepository.AddAsync(publication);
        if (_uow.HasChanges()) await _uow.Complete();
        return Ok("Publication added successfully");
    }
    /// <summary>
    /// Gets all publications based on provided filter criteria.
    /// </summary>
    /// <param name="filter">A PublicationsFilter object containing filtering options (optional).</param>
    /// <returns>An IActionResult object containing a PagedList of PublicationDTO objects or an error message.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(PublicationsFilter filter)
    {
        var publications = await _uow.publicationsRepository.GetAllByFilter(filter);

        Response.AddPaginationHeader(new Common.Helper.PaginationHeader
        (publications.CurrentPage, publications.PageSize, publications.TotalCount, publications.TotalPages));

        return Ok(publications);
    }
    /// <summary>
    /// Deletes a Publication by its ID.
    /// </summary>
    /// <param name="id">The ID of the publication to delete.</param>
    /// <returns>An IActionResult object indicating success or failure of the deletion.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMessage(int id)
    {
        if (id > 0) return BadRequest("Invalid id provided");

        await _uow.publicationsRepository.DeleteAsync(id);

        if (await _uow.Complete()) return Ok();
        return BadRequest("Problem while deleting publication");
    }

    /// <summary>
    /// Update publications data 
    /// </summary>
    /// <param name="pub"></param>
    /// <returns>Task: if it is complete or not </returns>
    [HttpPut]
    public async Task<IActionResult> Update(PublicationDTO pub)
    {
        var publication = await _uow.publicationsRepository.GetById(pub.Id, User.getID());

        if (publication == null) return BadRequest("Failed to update publication data");

        _mapper.Map(pub, publication);

        await _uow.publicationsRepository.UpdateAsync(publication);

        if (await _uow.Complete()) return Ok();

        return BadRequest("Some error occures while updating publicatons");

    }
    /// <summary>
    /// Action to add comments onto a publiation
    /// </summary>
    /// <param name="comment"></param>
    /// <returns>If adding successfull or not</returns>
    [HttpPost("addcomment")]
    public async Task<IActionResult> AddComment([FromBody] CommentDTO comment)
    {
        await _uow.publicationsRepository.AddCommentAsync(comment.PublicationId, comment.ViewerId ?? User.getID(), comment.Comment);

        if (await _uow.Complete()) return Ok();

        return BadRequest("Failed to add comment");
    }
    /// <summary>
    /// Get list of user comments onto a publication using filters 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>List of Comment dto </returns>
    [HttpGet("{id}/comments")]
    public async Task<IActionResult> GetComments(PublicationCommentFilter filter)
    {
        //TODO : This is not the best way to do this will rework on this and handle comment 
        var comments = await _uow.publicationsRepository.GetCommentsAsync(filter);
        Response.AddPaginationHeader(new Common.Helper.PaginationHeader
       (comments.CurrentPage, comments.PageSize, comments.TotalCount, comments.TotalPages));

        return Ok(comments);
    }

    /// <summary>
    /// Action to like a publication 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>If Complete or not</returns>
    [HttpPost("{id}/like")]
    public async Task<IActionResult> AddLike(int id)
    {
        if (id <= 0) return BadRequest("Invalid publication id");
        int viewerid = User.getID();
        if (viewerid <= 0) return BadRequest("failed to add like");
        await _uow.publicationsRepository.AddLikeAsync(id, viewerid);
        if (await _uow.Complete()) return Ok();
        return BadRequest("Failed to add like");
    }

    /// <summary>
    /// Upload attachments of an publication can be a pdf word or csvs
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFiles([FromForm] UploadFileDTO files)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)));
        }
        var uploadedFiles = new List<string>();
        var baseUrl = _info.ApplicationURL;
        if (files.PublicationId <= 0) return BadRequest("Publication ID is required");
        // getting the publications.
        var publication = await _uow.publicationsRepository.GetById(files.PublicationId, User.getID());

        foreach (var file in files.Files)
        {
            if (!file.IsAllowedContentType())
            {
                return BadRequest("Only PDF, Word, and Excel files are allowed.");
            }

            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{_info.AppName}_{timestamp}{Path.GetExtension(fileName)}";
            var filePath = Path.Combine("uploads/publications/", newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            string relativepath = $"/uploads/{newFileName}";
            var url = $"{baseUrl}{relativepath}";
            // updating the urls to the db 
            publication.AttachedDocumets.Add(relativepath);
            uploadedFiles.Add(url);
        }

        if (!await _uow.Complete()) return BadRequest("Failed to upload files");

        return Ok(new { uploadedFiles });
    }
    /// <summary>
    /// Adds a new Categorie to the data store.
    /// 
    /// This method validates the provided CategorieDTO object and then calls the UnitOfWork's
    /// publicationsRepository to add the Categorie. If successful, it returns a 200 OK response
    /// indicating successful creation.
    /// </summary>
    /// <param name="dto">The CategorieDTO object representing the new categorie to be added.</param>
    /// <returns>
    ///   An IActionResult object:
    ///     - 200 OK with message "Categorie inserted successfully" on success.
    ///     - 400 BadRequest with validation errors or "Categorie data can't be null" message.
    /// </returns>
    [HttpPost("addCategorie")]
    public async Task<IActionResult> AddCategorie(CategorieDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)));
        }

        if (dto == null) return BadRequest("Categorie data can't be null");

        await _uow.publicationsRepository.AddCategorie(dto);

        if (_uow.HasChanges()) await _uow.Complete();

        return Ok("Categorie inserted successfully");
    }
    /// <summary>
    /// Updates an existing Categorie in the data store.
    /// 
    /// This method validates the provided CategorieDTO object and then calls the UnitOfWork's
    /// publicationsRepository to update the Categorie. If successful, it returns a 200 OK response
    /// indicating successful update.
    /// </summary>
    /// <param name="categorie">The CategorieDTO object with updated information.</param>
    /// <returns>
    ///   An IActionResult object:
    ///     - 200 OK with message "Categorie updated successfully" on success.
    ///     - 400 BadRequest with validation errors or "Categorie data can't be null" message.
    /// </returns>
    [HttpPut("updateCategorie")]
    public async Task<IActionResult> UpdateCategorie(CategorieDTO categorie)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)));
        }

        if (categorie == null) return BadRequest("Categorie data can't be null");

        await _uow.publicationsRepository.UpdateCategorieAsync(categorie);

        if (_uow.HasChanges()) await _uow.Complete();

        return Ok("categorie updated successfully");
    }
    /// <summary>
    /// Deletes a Categorie from the data store by its ID.
    /// 
    /// This method checks if the provided ID is valid and then calls the UnitOfWork's
    /// publicationsRepository to delete the Categorie. If successful, it returns a 200 OK response
    /// indicating successful deletion.
    /// </summary>
    /// <param name="id">The ID of the Categorie to be deleted.</param>
    /// <returns>
    ///   An IActionResult object:
    ///     - 200 OK with message "Categorie Deleted successfully" on success.
    ///     - 400 BadRequest with message "Categorie id is required" or "Categorie data not found".
    /// </returns>
    [HttpDelete("removeCategorie/{id}")]
    public async Task<IActionResult> RemoveCategorie(int id)
    {
        if (id <= 0) return BadRequest("Categorie id is required");

        if (!await _uow.publicationsRepository.IsCategorieExists(id))
            return BadRequest("Categorie data not found");

        await _uow.publicationsRepository.DeleteCategorieAsync(id);

        if (_uow.HasChanges()) await _uow.Complete();

        return Ok("Categorie Deleted successfully");
    }
    /// <summary>
    /// Gets a Categorie by its ID.
    /// 
    /// This method calls the UnitOfWork's publicationsRepository to specific categories by id.
    /// If successful, it returns a 200 OK response with a collection of CategorieDTO objects.
    /// If no categories are found, it returns a 400 BadRequest with a message indicating no data found.
    /// </summary>
    /// <returns>
    ///   An IActionResult object:
    ///     - 200 OK with a collection of CategorieDTO objects on success.
    ///     - 400 BadRequest with message "No categorie data found" if no categories exist.
    /// </returns>
    [HttpGet("categorie/{id}")]
    public async Task<IActionResult> GetCategorie(int id)
    {
        if (id <= 0) return BadRequest("Categorie id is required");

        if (!await _uow.publicationsRepository.IsCategorieExists(id))
            return BadRequest("Categorie data not found");
        var categorie = await _uow.publicationsRepository.GetCategorieByID(id);
        return Ok(categorie);
    }
    /// <summary>
    /// Gets all Categorie objects from the data store.
    /// 
    /// This method calls the UnitOfWork's publicationsRepository to retrieve all categories.
    /// If successful, it returns a 200 OK response with a collection of CategorieDTO objects.
    /// If no categories are found, it returns a 400 BadRequest with a message indicating no data found.
    /// </summary>
    /// <returns>
    ///   An IActionResult object:
    ///     - 200 OK with a collection of CategorieDTO objects on success.
    ///     - 400 BadRequest with message "No categorie data found" if no categories exist.
    /// </returns>
    [HttpGet("categorie")]
    public async Task<IActionResult> GetAllCategorie()
    {
        var categories = await _uow.publicationsRepository.GetAllCategorie();

        if (categories.Count() <= 0) return BadRequest("No categorie data found");

        return Ok(categories);
    }
}