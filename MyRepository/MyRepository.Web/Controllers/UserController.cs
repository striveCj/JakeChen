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
            entityList = _repository.Table.ToList();
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
                iTotalRecords = 50,
                iTotalDisplayRecords = 50,
                aaData = dtoList
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
