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
        private readonly EfRepository<UserEntity> _efRepository;

        public UserService()
        {
            this._efRepository = _unitOfWork.Repository<UserEntity>(); 
        }
        public UserService(EfRepository<UserEntity> efRepository)
        {
            _efRepository = efRepository;
        }

        public void Delete(Guid id)
        {
            if (id == default(Guid))
            {
                throw new ArgumentException("id不能是Guid默认值", nameof(id));
            }
            UserEntity userEntity = _efRepository.GetById(id);
            userEntity.IsDelete = true;
            userEntity.ModifyDateTime = DateTime.Now;
            _efRepository.Update(userEntity);
        }

        public List<UserEntity> GetList()
        {
            return _efRepository.Table.ToList();
        }
        public ResultListDto GetList(DataTableParameter param)
        {
            int listCount = _efRepository.Table.Count();
            var entityList = string.IsNullOrEmpty(param.sSearch) ? _efRepository.Table.OrderBy(m => m.CreateDateTime).Skip((param.iDisplayStart / param.iDisplayLength) * param.iDisplayLength).Take(param.iDisplayLength).ToList() : _efRepository.Table.Where(item => item.UserName.Contains(param.sSearch)).OrderBy(m => m.CreateDateTime).Skip((param.iDisplayStart / param.iDisplayLength) * param.iDisplayLength).Take(param.iDisplayLength).ToList();
            var userDtoList = AutoMapper.Mapper.Map<List<UserDto>>(entityList);
            return new ResultListDto
            {
                sEcho = param.sEcho,
                iTotalRecords = listCount,
                iTotalDisplayRecords = listCount,
                aaData = userDtoList
            };
        }

        public UserDto GetModel(Guid id)
        {
            UserEntity userEntity = _efRepository.GetById(id);
            var userDto = AutoMapper.Mapper.Map<UserDto>(userEntity);
            return userDto;
        }

        public void Update(UserDto dto)
        {
            UserEntity userEntity = _efRepository.GetById(dto.Id);
            userEntity.UserName = dto.UserName;
            userEntity.Email = dto.Email;
            userEntity.Password = dto.Password;
            _efRepository.Update(userEntity);
        }
    }
}
