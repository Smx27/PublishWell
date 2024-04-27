using AutoMapper;
using JPS.Data.Entities;
using PublishWell.API.Controllers.Authentication.DTO;
using PublishWell.API.Controllers.Mail.DTO;
using PublishWell.API.Controllers.Publications.DTO;
using PublishWell.API.Controllers.Users.DTO;
using PublishWell.API.Data.Entities;

namespace JPS.API.Common.Helper
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
            // Publication mapping 
            CreateMap<Publication, PublicationDTO>()
                .ForMember(d=> d.ViewCount , opt=> opt.MapFrom(src=> src.PublicationViews.Count))
                .ForMember(d=> d.LikeCount , opt=> opt.MapFrom(src=> src.PublicationLikes.Count))
                .ForMember(d=> d.CommentCount , opt=> opt.MapFrom(src=> src.PublicationComments.Count));
            CreateMap<PublicationDTO, Publication>();

            CreateMap<PublicationComment, CommentDTO>().ReverseMap();
            CreateMap<Categorie, CategorieDTO>().ReverseMap();

        }
    }
}
