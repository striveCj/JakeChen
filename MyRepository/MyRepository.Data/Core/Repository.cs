using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using MyRepository.Models.Base;

namespace MyRepository.Data.Core
{
    public class Repository<T> where T : BaseEntity
    {
        /// <summary>
        /// 数据上下文变量
        /// </summary>
        private readonly BaseContext _context;
        public Repository(BaseContext context)
        {
            this._context = context;
        }
        string _errorMessage = string.Empty;

        #region 封装属性
        /// <summary>
        /// 实体访问字段
        /// </summary>
        private IDbSet<T> _entities;



        /// <summary>
        /// 封装属性
        /// </summary>
        public IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    return _entities = _context.Set<T>();
                }
                else
                {
                    return _entities;
                }
            }
        }
        #endregion

        #region 泛型方法--根据Id查找实体
        /// <summary>
        /// 泛型方法--根据Id查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int id)
        {
            return this._entities.Find(id);
        }
        #endregion

        #region Insert
        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                else
                {
                    this.Entities.Add(entity);//把实体的状态设置为Added
                    entity.CreateDateTime = DateTime.Now;
                    entity.ModifyDateTime=DateTime.Now;
                    
                    this._context.SaveChanges();//保存到数据库
                }
            }
            catch (DbEntityValidationException dbEx)//DbEntityValidationException
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var item in validationErrors.ValidationErrors)
                    {
                        _errorMessage += string.Format("Property:{0} Error:{1}", item.PropertyName, item.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }
        #endregion

        #region Update
        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                else
                {
                    this._context.SaveChanges();//保存到数据库
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var error in validationErrors.ValidationErrors)
                    {
                        _errorMessage += string.Format("Property:{0} Error:{1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
                    }
                }
                //抛出捕获的异常
                throw new Exception(_errorMessage, dbEx);
            }
        }
        #endregion

        #region Delete
        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentException(nameof(entity));
                }
                else
                {
                    this._context.Entry(entity).State = EntityState.Deleted;
                    this._context.SaveChanges();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var error in validationErrors.ValidationErrors)
                    {
                        _errorMessage += string.Format("Property:{0} Error:{1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
                    }
                }

                throw new Exception(_errorMessage, dbEx);
            }
        }
        #endregion

        #region Table
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }
        #endregion

    }
}
