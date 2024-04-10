using AutoMapper;
using JPS.Common;
using JPS.Data.Entities;
using JPS.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublishWell.Controllers.Authentication.DTO;
using PublishWell.Controllers.Users.DTO;

namespace JPS.Controllers.Authentication
{
    /// <summary>
    /// This controller will handle the authentication
    /// </summary>
    public class AuthController : BaseAPIController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;


        /// <summary>
        /// Constructor to handle all the data initialization
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="tokenService"></param>
        /// <param name="mapper"></param>
        /// <param name="roleManager"></param>
        /// <param name="signInManager"></param>
        public AuthController(UserManager<AppUser> userManager, ITokenService tokenService, 
        IMapper mapper, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager
        )
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            
        }
        /// <summary>
        /// Api endpoint to register a new user into the system
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO register)
        {
            if(! await _roleManager.Roles.AnyAsync())
            {
                var roles = new List<AppRole>{
                    new AppRole{Name = "Member"},
                    new AppRole{Name = "Admin"},
                    new AppRole{Name = "Moderator"},
                    };
                foreach (var role in roles)
                {
                    await _roleManager.CreateAsync(role);
                }
            }
            if(await UserExist(register.Username)) return BadRequest("Username Taken");

            var user = _mapper.Map<AppUser>(register);

            var result = _userManager.CreateAsync(user, register.Password);

            if(!result.IsCompletedSuccessfully) return BadRequest(result.Exception.Message);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if(!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            return new UserDTO{
                Email = user.Email,
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }
        /// <summary>
        /// Api endpoint to login into the system
        /// </summary>
        /// <param name="login"></param>
        /// <returns>User Data</returns>
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login)
        {
            var user = await _userManager.Users
            .SingleOrDefaultAsync(u=> u.UserName.ToLower() == login.UserName.ToLower());

            if(user == null) return Unauthorized();
            var result = await _userManager.CheckPasswordAsync(user, login.Password);

            if(!result) return Unauthorized("Invalid Password");

            return new UserDTO{
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Email = user.Email
            };
        }
        
        /// <summary>
        /// Action to rest password of a user  
        /// </summary>
        /// <param name="model"></param>
        /// <returns>User DTO</returns>
        [HttpPost("forgetPassword")]
        public async Task<ActionResult<UserDTO>> ForgetPassword(ForgetPasswordDTO model)
        {
            //Getting the user 
            var user = getUserForPasswordrReset(model);
            if(user != null)
            {
                IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(user, model.ResetToken, model.NewPassword);
                if(passwordChangeResult.Succeeded) return Ok(_mapper.Map <UserDTO>(user));
                if(passwordChangeResult.Errors.Any()) return BadRequest(passwordChangeResult.Errors);
            }
            return null;

        }

        /// <summary>
        /// Generate Password reset token
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Forget Password reset token</returns>
        [HttpPost("generateResetToken")]
        public async Task<ActionResult<ForgetPasswordDTO>> GenerateResetToken(ForgetPasswordDTO model)
        {
            //Getting the user 
            var user = getUserForPasswordrReset(model);
            if(user != null)
            {
                model.ResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            }
            //Now sending the model for demo Mail service will be added later
            return Ok(model);
        }

        /// <summary>
        /// Get user for Forgot password reset
        /// </summary>
        /// <param name="model"></param>
        /// <returns>AppUser object if user exists else null</returns>
        private AppUser getUserForPasswordrReset(ForgetPasswordDTO model)
        {
            if(Utilities.IsNotNullOrEmpty(model.Id))
            {
                return _userManager.FindByIdAsync(model.Id).Result;
            }
            else if(Utilities.IsNotNullOrEmpty(model.Email))
            {
                return _userManager.FindByEmailAsync(model.Email).Result;            
            }
            else if (Utilities.IsNotNullOrEmpty(model.UserName))
            {
                return _userManager.FindByNameAsync(model.UserName).Result;
            }
            return null;
        }
        /// <summary>
        /// Method To check is user exist in DB 
        /// </summary>
        /// <param name="username"></param>
        /// <returns>True/False</returns>
        private async Task<bool> UserExist(string username)
        {
            return await _userManager.Users.AnyAsync(u=> u.UserName == username.ToLower());
        }
    }
}