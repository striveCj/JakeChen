using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MyRepository.Models.Entities;

namespace MyRepository.Models.Mapping
{
    public class UserInfoMap: EntityTypeConfiguration<UserInfoEntity>
    {
        //public UserInfoMap()
        //{
        //    this.ToTable("T_USER_INFO");
        //    this.HasKey(s => s.Id);
        //    this.Property(s => s.Email).HasColumnType("varchar").HasMaxLength(50);
        //    this.Property(s => s.Password).HasColumnType("varchar").HasMaxLength(50).IsRequired();
        //    this.Property(s => s.UserName).HasColumnType("varchar").HasMaxLength(50).IsRequired();



        //}
    }
}
