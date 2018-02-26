using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRepository.Models.Entities;

namespace MyRepository.Models.Mapping
{
     public class UserMap : EntityTypeConfiguration<UserEntity>
    {
        public UserMap()
        {
            //配置主键
            this.HasKey(s => s.Id);
            this.Property(s => s.Email).HasColumnType("varchar").HasMaxLength(50);
            this.Property(s => s.Password).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            this.Property(s => s.UserName).HasColumnType("varchar").HasMaxLength(50).IsRequired();

            //配置表名
            this.ToTable("User");
        }
    }
}
