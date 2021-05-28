using AutoMapper;
using DTO;
using Infrastructure.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<PostDTO, Post>();
            CreateMap<User, UserDTO>();
        }
    }
}