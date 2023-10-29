using AutoMapper;
using Microsoft.Extensions.Hosting;
using MyBlog.BLL.ViewModels.Articles.Request;
using MyBlog.BLL.ViewModels.Comments.Request;
using MyBlog.BLL.ViewModels.Tags.Request;
using MyBlog.BLL.ViewModels.Users.Request;
using MyBlog.Data.Models.Articles;
using MyBlog.Data.Models.Comments;
using MyBlog.Data.Models.Tags;
using MyBlog.Data.Models.Users;

namespace MyBlog.App.Extentions
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<RegisterUserViewModel, User>()
                    .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email))
                    .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
            CreateMap<EditUserViewModel, User>();
            CreateMap<CreateCommentViewModel, Comment>();
            CreateMap<EditCommentViewModel, Comment>();
            CreateMap<CreateArticleViewModel, Article>();
            CreateMap<EditArticleViewModel, Article>();
            CreateMap<CreateTagViewModel, Tag>();
            CreateMap<EditTagViewModel, Tag>();
        }
    }
}