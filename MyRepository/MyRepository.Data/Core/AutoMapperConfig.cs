using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyRepository.Models.Dto;
using MyRepository.Models.Entities;

namespace MyRepository.Data.Core
{
    public static class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(x=>x.CreateMap<UserEntity,UserDto>());
        }
    }
}
