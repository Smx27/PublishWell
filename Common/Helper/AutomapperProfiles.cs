using AutoMapper;
using JPS.Data.Entities;
using PublishWell.Controllers.Authentication.DTO;
using PublishWell.Controllers.Users.DTO;

namespace JPS.Common.Helper
{
    /// <summary>
    /// This class configures AutoMapper mappings for the application.
    /// </summary>
    public class AutomapperProfiles : Profile
    {
        /// <summary>
        /// Profile COnstructor
        /// </summary>
        public AutomapperProfiles()
        {
            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            CreateMap<AppUser, UserDataDTO>().ReverseMap();

        }
    }
}
