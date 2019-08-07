using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models.Account;
using WebApi.Models.Comment;
using WebApi.Models.Post;
using WebApi.Models.User;

namespace WebApi.Mapper
{
    public class MapViewModels : AutoMapper.Profile
    {
        public MapViewModels()
        {
            CreateMap<RegisterViewModel, DTOUser>();
            CreateMap<DTOUser, ProfileViewModel>();
            CreateMap<ChangeProfileViewModel, DTOUser>();

            CreateMap<DTOPost, PostViewModel>();
            CreateMap<PostEditViewModel, DTOPost>();

            CreateMap<DTOComment, CommentViewModel>();
            CreateMap<CommentEditViewModel, DTOComment>();

           


        }
    }
}