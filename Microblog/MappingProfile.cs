using AutoMapper;
using BdService.BLL.ViewModels.Comments;
using BdService.BLL.ViewModels.Posts;
using BdService.BLL.ViewModels.Tags;
using BdService.BLL.ViewModels.Users;
using BdService.DAL.Models;

namespace Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterViewModel, User>()
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email))
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.UserName));
            CreateMap<UserEditViewModel, User>(); 
            CreateMap<CommentCreateViewModel, Comment>();
            CreateMap<CommentEditViewModel, Comment>();
            CreateMap<PostCreateViewModel, Post>();
            CreateMap<PostEditViewModel, Post>();
            CreateMap<TagCreateViewModel, Tag>();
            CreateMap<TagEditViewModel, Tag>();
        }
    }
}
