using AutoMapper;
using JPS.Data.Entities;
using PublishWell.Controllers.Authentication.DTO;
using PublishWell.Controllers.Mail.DTO;
using PublishWell.Controllers.Users.DTO;
using PublishWell.Data.Entities;

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
            CreateMap<MailTemplate, MailDTO>()
                .ForMember(dest=> dest.TemplateID, opt => opt.MapFrom(src=> src.Id))
                .ForMember(dest=> dest.TemplateTypeID, opt => opt.MapFrom(src=> src.Type.Id))
                .ForMember(d=> d.TemplateTypeName, opt => opt.MapFrom(src=> src.Type.Name));
            CreateMap<MailDTO, MailTemplate>();
            CreateMap<MailTemplateType, TemplateTypeDTO>().ReverseMap();

        }
    }
}
