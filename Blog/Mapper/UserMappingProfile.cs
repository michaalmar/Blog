using AutoMapper;
using Blog.DTO;
using Blog.Models;

namespace Blog.Mapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}