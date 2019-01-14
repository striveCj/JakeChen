using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyRepository.Data.Core;
using MyRepository.Data.User;
using MyRepository.Models.Base;
using MyRepository.Models.Dto;
using MyRepository.Models.Entities;

namespace MyRepository.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View(_userService.GetList());
        }

        public JsonResult GetList(DataTableParameter param)
        {
           
            return Json(_userService.GetList(param), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(Guid id)
        {
            _userService.Delete(id);
            return Json(new {message = "删除成功"});
        }

        public JsonResult GetModel(Guid id)
        {
            UserDto userDto = _userService.GetModel(id);
            return Json(userDto);
        }
        public JsonResult Update(UserDto dto)
        {
          _userService.Update(dto);
            return Json(new {message="修改成功"});
        }
    }
}
