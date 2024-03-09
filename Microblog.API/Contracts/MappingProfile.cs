using AutoMapper;
using BdService.BLL.ViewModels.Comments;
using BdService.BLL.ViewModels.Posts;
using BdService.BLL.ViewModels.Tags;
using BdService.BLL.ViewModels.Users;
using BdService.DAL.Models;

namespace Microblog.API.Contracts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterViewModel, User>()
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email))
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.UserName));

            CreateMap<CommentCreateViewModel, Comment>();
            CreateMap<CommentEditViewModel, Comment>();
            CreateMap<PostCreateViewModel, Post>();
            CreateMap<PostEditViewModel, Post>();
            CreateMap<TagCreateViewModel, Tag>();
            CreateMap<TagEditViewModel, Tag>();
            CreateMap<UserEditViewModel, User>();
        }
    }
}
