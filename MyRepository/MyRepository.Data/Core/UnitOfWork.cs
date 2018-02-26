using System;
using System.Collections.Generic;
using MyRepository.Models.Base;

namespace MyRepository.Data.Core
{
    public class UnitOfWork : IDisposable
    {
        private readonly BaseContext _context;
        private bool _disposed;
        private Dictionary<string, object> _repositories;

        public UnitOfWork(BaseContext context)
        {
            this._context = context;  //构造函数中初始化上下文对象
        }

        public UnitOfWork()
        {
            _context = new BaseContext(); //构造函数中初始化上下文对象
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        #endregion

        #region Save
        public void Save()
        {
            _context.SaveChanges();
        }
        #endregion

        #region Repository<T>()
        public Repository<T> Repository<T>() where T : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();

            }
            var type = typeof(T).Name;//获取当前成员名称
            if (!_repositories.ContainsKey(type))//如果repositories中不包含Name
            {
                var repositoryType = typeof(Repository<>);//获取Repository<>类型
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);

            }
            return (Repository<T>)_repositories[type];

        }
        #endregion


    }
}
