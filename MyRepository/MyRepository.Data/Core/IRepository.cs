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
        /// <summary>
        /// 得到可以查询的 <see cref="System.Linq.IQueryable{T}"/> 对象。
        /// </summary>
        IQueryable<T> Table { get; }
        /// <summary>
        /// 得到可以查询的 <see cref="System.Linq.IQueryable{T}"/> 对象，但是不监控实体的状态。
        /// </summary>
        IQueryable<T> TableNoTracking { get; }
    }
}
