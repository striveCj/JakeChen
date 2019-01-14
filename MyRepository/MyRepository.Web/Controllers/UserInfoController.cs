using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyRepository.Data.Core;
using MyRepository.Models.Entities;

namespace MyRepository.Web.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly Repository<UserInfoEntity> _repository;

        public UserInfoController()
        {
            this._repository = _unitOfWork.Repository<UserInfoEntity>(); ;
        }
        // GET: UserInfo
        public ActionResult Index()
        {
            var list = _repository.Entities.ToList();
            return View(list);
        }
    }
}