using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRepository.Models.Base;
using MyRepository.Models.Dto;
using MyRepository.Models.Entities;

namespace MyRepository.Data.User
{
    public interface IUserService
    {
        void Update(UserDto dto);
        UserDto GetModel(Guid id);
        void Delete(Guid id);
        ResultListDto GetList(DataTableParameter param);

        List<UserEntity> GetList();
    }
}
