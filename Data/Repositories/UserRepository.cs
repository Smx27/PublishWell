using AutoMapper;
using AutoMapper.QueryableExtensions;
using JPS.Data.Entities;
using JPS.Interfaces;
using Microsoft.EntityFrameworkCore;
using PublishWell.Controllers.Users.DTO;

namespace JPS.Data.Repositories
{
    /// <summary>
    /// User repository to handle User interactions, providing data access for retrieving user information.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Database context used for accessing user data.
        /// </summary>
        public DataContext _context { get; }
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class with a specified data context.
        /// </summary>
        /// <param name="context">The data context to use for data access.</param>
        /// <param name="mapper"></param>
        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a user by their ID asynchronously.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A task that resolves to an `UserDataDTO` object representing the retrieved user, or null if not found.</returns>
        public async Task<UserDataDTO> GetUserByID(int userID)
        {
            return _mapper.Map<UserDataDTO>(await _context.Users.FindAsync(userID));    
        }

        /// <summary>
        /// Gets all users asynchronously.
        /// </summary>
        /// <returns>A task that resolves to a list of `AppUser` objects representing all retrieved users.</returns>
        public async Task<List<UserDataDTO>> GetAllUsers()
        {
            var users = await _context.Users
                .Where(u=> !u.IsDeleted)
                .OrderBy(u => u.Id)
                .ProjectTo<UserDataDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return users;
        }
        /// <summary>
        /// Gets a user by their username asynchronously, but this method has a potential typo in its name and parameter.
        /// </summary>
        /// <param name="userName">The username of the user (likely a typo, should be string name instead of int userID).</param>
        /// <returns>A task that resolves to an `AppUser` object representing the retrieved user, or null if not found.</returns>
        public async Task<UserDataDTO> GetUserByName(string userName)  
        {
            return await _context.Users
                .Where(u => u.UserName == userName)
                .ProjectTo<UserDataDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        /// <summary>
        /// Gets a user by their email address asynchronously, but this method has a potential typo in its name and parameter.
        /// </summary>
        /// <param name="email">The email address of the user (likely a typo, should be string email instead of int userID).</param>
        /// <returns>A task that resolves to an `AppUser` object representing the retrieved user, or null if not found.</returns>
        public async Task<UserDataDTO> GetUserByEmail(string email) 
        {
            return await _context.Users
                .Where(u => u.UserName == email)
                .ProjectTo<UserDataDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }
        /// <summary>
        /// Update userdata 
        /// </summary>
        /// <param name="userdata"></param>
        public async Task UpdateAsync(UserDataDTO userdata)
        {
            var user = await _context.Users.Where(u=> u.UserName == userdata.UserName).SingleOrDefaultAsync();
            if (user != null)
            {
                _mapper.Map(userdata, user);

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User not found");
            }
            
        }
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        public async Task CreateAsync(UserDataDTO user)
        {
            if(user != null)
            {
                await _context.Users.AddAsync(_mapper.Map<AppUser>(user));
            }
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Delete user from database
        /// </summary>
        /// <param name="userID"></param>        
        public Task DeleteAsync(int userID)
        {
            _context.Users.Where(u=> u.Id == userID)
            .SingleOrDefault().IsDeleted = true;
            _context.SaveChanges();
            return Task.CompletedTask;
        }
        /// <summary>
        /// Check if user exist
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool IsExist(int userID)
        {
            var user = _context.Users.Find(userID);
            if (user != null)
                return true;
            else
                return false;
        }
    }
}
