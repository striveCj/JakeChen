using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using MyRepository.Models.Entities;
using MyRepository.Models.Mapping;

namespace MyRepository.Data.Core
{
    public class BaseContext:DbContext
    {
        public BaseContext() : base("name=Default")
        {
            
        }

        //public DbSet<UserEntity> User { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //IList<Assembly> assemblies = EngineContext.Current.Resolve<ITypeFinder>().GetAssemblies();
            //foreach (Assembly assembly in assemblies)
            //{
            //    var typesToRegister = assembly.GetTypes().Where(type => !String.IsNullOrEmpty(type.Namespace)).Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            //    foreach (var type in typesToRegister)
            //    {
            //        dynamic configurationInstance = Activator.CreateInstance(type);
            //        modelBuilder.Configurations.Add(configurationInstance);
            //    }
            //    //...or do it manually below. For example,
            //    //modelBuilder.Configurations.Add(new LanguageMap());
            //}


            //var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            //    .Where(type => !String.IsNullOrEmpty(type.Namespace))
            //    .Where(type => type.BaseType != null && type.BaseType.IsGenericType
            //                   && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
            //    modelBuilder.Configurations.Add(configurationInstance);
            //}
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();  //去除“设置表名为复数”这条约定
            modelBuilder.Configurations.Add(new UserMap());

        }
    }
}
