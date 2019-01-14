using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyRepository.Models.Base;

namespace MyRepository.Models.Entities
{
    public class UserInfoEntity : BaseEntity
    {
        [Column("UserId")]
        public override Guid Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

    }
}
