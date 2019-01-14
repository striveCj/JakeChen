using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRepository.Models.Base;

namespace MyRepository.Data.Core
{
    public interface IRepository<T> where T :BaseEntity
    {
        T GetById(Guid id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
