using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MyRepository.Data.Core;
using MyRepository.Models.Dto;
using MyRepository.Models.Entities;

namespace MyRepository.Web.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// 登录注册控制器
    /// </summary>
    public class LoginController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly EfRepository<UserEntity> _efRepository;

        public LoginController()
        {
            this._efRepository = _unitOfWork.Repository<UserEntity>(); ;
        }

        //登录视图
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 注册视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        public JsonResult Login(LoginDto dto)
        {
            UserEntity userEntity = _efRepository.Entities.FirstOrDefault(item => item.UserName== dto.UserName);
            if (userEntity==null)
            {
                return Json(new
                {
                    successful = false,
                    message = "用户不存在"
                });
            }
            else
            {
                if (userEntity.Password!=dto.Password)
                {
                    return Json(new
                    {
                        successful = false,
                        message = "用户密码错误"
                    });
                }
                else
                {
                    return Json(new
                    {
                        successful = true,
                        data = userEntity
                    });
                }
            }
        }

        public JsonResult UserRegister(UserDto dto)
        {
            if (dto==null)
            {
               throw new ArgumentNullException(nameof(dto));
            }
            UserEntity userEntity = new UserEntity
            {
                Id = Guid.NewGuid(),
                UserName = dto.UserName,
                Password = dto.Password,
                Email = dto.Email
            };
            _efRepository.Insert(userEntity);
            return Json(new { successful = true,message = "添加成功"});
        }
        /// <summary>
        /// 验证测试视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ValidatorTest()
        {
            return View();
        }

    }
}