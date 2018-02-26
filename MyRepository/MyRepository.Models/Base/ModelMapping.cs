using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyRepository.Models.Dto;
using MyRepository.Models.Entities;

namespace MyRepository.Models.Base
{
     public class ModelMapping: IStartupTask
    {
        public void Execute()
        {
            Create<UserEntity, UserDto>();
        }
        protected virtual void Create<T1, T2>()
        {
            Mapper.Initialize(x => x.CreateMap <T1,T2>());
            Mapper.Initialize(x => x.CreateMap <T2,T1>());
        }
    }
}
