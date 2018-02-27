using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyRepository.Data.Core;
using MyRepository.Models.Base;
using MyRepository.Models.Dto;
using MyRepository.Models.Entities;

namespace MyRepository.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly Repository<UserEntity> _repository;

        public UserController()
        {
            this._repository = _unitOfWork.Repository<UserEntity>(); ;
        }


        // GET: User
        public ActionResult Index()
        {
            return View(_repository.Table.ToList());
        }

        public JsonResult GetList(DataTableParameter param)
        {
            List<UserEntity> entityList=new List<UserEntity>();
            List <UserDto> dtoList =new List<UserDto>();
            int listCount = _repository.Table.Count();

            if (string.IsNullOrEmpty(param.sSearch))
            {
                entityList = _repository.Table.OrderBy(m => m.CreateDateTime).Skip((param.iDisplayStart / param.iDisplayLength) * param.iDisplayLength).Take(param.iDisplayLength).ToList();
            }
            else
            {
                entityList = _repository.Table.Where(item=>item.UserName.Contains(param.sSearch)).OrderBy(m => m.CreateDateTime).Skip((param.iDisplayStart / param.iDisplayLength) * param.iDisplayLength).Take(param.iDisplayLength).ToList();
            }
           
            foreach (var item in entityList)
            {
                UserDto dto=new UserDto()
                {
                    Id =item.Id,
                    UserName = item.UserName,
                    Password = item.Password,
                    Email = item.Email
                };
                dtoList.Add(dto);
            }
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = listCount,
                iTotalDisplayRecords = listCount,
                aaData = dtoList
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(Guid id)
        {
            if (id==default(Guid))
            {
                throw new ArgumentException("id不能是Guid默认值",nameof(id));
            }
            UserEntity userEntity = _repository.GetById(id);
            userEntity.IsDelete = true;
            userEntity.ModifyDateTime=DateTime.Now;
            _repository.Update(userEntity);
            return Json(new {message = "删除成功"});
        }

        public JsonResult GetModel(Guid id)
        {
            UserEntity userEntity = _repository.GetById(id);
            UserDto userDto=new UserDto()
            {
                Id=userEntity.Id,
                UserName = userEntity.UserName,
                Password = userEntity.Password,
                Email = userEntity.Email
            };
            return Json(userDto);
        }
        public JsonResult Update(UserDto dto)
        {
            UserEntity userEntity = _repository.GetById(dto.Id);
            userEntity.UserName = dto.UserName;
            userEntity.Email = dto.Email;
            userEntity.Password = dto.Password;
            _repository.Update(userEntity);
            return Json(new {message="修改成功"});
        }
    }
}
