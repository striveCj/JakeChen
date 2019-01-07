using System;
using System.Collections.Generic;
using MyRepository.Data.User;
using MyRepository.Models.Base;
using MyRepository.Models.Dto;

namespace MyRepository.DAL.User
{
    public class UserService : IUserService
    {
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<UserDto> GetList(DataTableParameter param)
        {
            throw new NotImplementedException();
        }

        public MyRepository.Models.Dto.UserDto GetModel(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(MyRepository.Models.Dto.UserDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
