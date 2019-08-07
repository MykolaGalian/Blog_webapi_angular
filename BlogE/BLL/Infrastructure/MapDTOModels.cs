using AutoMapper;
using BLL.DTO;
using DAL.Identity;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{

    public class MapDTOModels : Profile
    {
        public MapDTOModels()
        {
            CreateMap<Comment, DTOComment>().ReverseMap(); // bidirectional mapping
            CreateMap<Post, DTOPost>().ReverseMap();
            CreateMap<UserInfo, DTOUser>().ReverseMap();
            CreateMap<ApplicationUser, DTOUser>();

            //the set of properties of the ApplicationUser and DTOUser models is different,
            //so we need to use the additional ForMember () method to map properties of one class onto properties of another class
            CreateMap<DTOUser, ApplicationUser>().ForMember(x => x.UserName, opt => opt.MapFrom(y => y.Login))
                .ForMember(x => x.Roles, opt => opt.Ignore());  //ignore Roles (string type)
        }
    }
}
