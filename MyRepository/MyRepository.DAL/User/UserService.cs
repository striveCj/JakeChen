using System;
using System.Collections.Generic;
using System.Linq;
using MyRepository.Data.Core;
using MyRepository.Data.User;
using MyRepository.Models.Base;
using MyRepository.Models.Dto;
using MyRepository.Models.Entities;

namespace MyRepository.DAL.User
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly Repository<UserEntity> _repository;

        public UserService()
        {
            this._repository = _unitOfWork.Repository<UserEntity>(); 
        }
        public UserService(Repository<UserEntity> repository)
        {
            _repository = repository;
        }

        public void Delete(Guid id)
        {
            if (id == default(Guid))
            {
                throw new ArgumentException("id不能是Guid默认值", nameof(id));
            }
            UserEntity userEntity = _repository.GetById(id);
            userEntity.IsDelete = true;
            userEntity.ModifyDateTime = DateTime.Now;
            _repository.Update(userEntity);
        }

        public List<UserEntity> GetList()
        {
            return _repository.Table.ToList();
        }
        public ResultListDto GetList(DataTableParameter param)
        {
            List<UserDto> dtoList = new List<UserDto>();
            int listCount = _repository.Table.Count();

            var entityList = string.IsNullOrEmpty(param.sSearch) ? _repository.Table.OrderBy(m => m.CreateDateTime).Skip((param.iDisplayStart / param.iDisplayLength) * param.iDisplayLength).Take(param.iDisplayLength).ToList() : _repository.Table.Where(item => item.UserName.Contains(param.sSearch)).OrderBy(m => m.CreateDateTime).Skip((param.iDisplayStart / param.iDisplayLength) * param.iDisplayLength).Take(param.iDisplayLength).ToList();

            foreach (var item in entityList)
            {
                UserDto dto = new UserDto()
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Password = item.Password,
                    Email = item.Email
                };
                dtoList.Add(dto);
            }

            return new ResultListDto
            {
                sEcho = param.sEcho,
                iTotalRecords = listCount,
                iTotalDisplayRecords = listCount,
                aaData = dtoList
            };
        }

        public UserDto GetModel(Guid id)
        {
            UserEntity userEntity = _repository.GetById(id);
            UserDto userDto = new UserDto()
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
                Password = userEntity.Password,
                Email = userEntity.Email
            };
            return userDto;
        }

        public void Update(UserDto dto)
        {
            UserEntity userEntity = _repository.GetById(dto.Id);
            userEntity.UserName = dto.UserName;
            userEntity.Email = dto.Email;
            userEntity.Password = dto.Password;
            _repository.Update(userEntity);
        }
    }
}
