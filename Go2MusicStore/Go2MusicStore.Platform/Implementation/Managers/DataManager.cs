namespace Go2MusicStore.Platform.Implementation.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.Platform.Interfaces.DataLayer;

    public abstract class DataManager : IManager, IDisposable
    {
        private readonly IUnitOfWork unitOfWork;

        private bool isDisposed = false;

        protected DataManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;            
        }

        public void Add<T>(T model) where T : class
        {
            this.unitOfWork.GetRepository<T>().Insert(model);
        }

        public IEnumerable<T> Get<T>() where T : class
        {
            return this.unitOfWork.GetRepository<T>().Get();
        }

        public virtual IEnumerable<T> Get<T>(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "") where T : class
        {
            return this.unitOfWork.GetRepository<T>()
                .Get(filter, orderBy, includeProperties);
        }

        public void Update<T>(T model) where T : class
        {
            this.unitOfWork.GetRepository<T>().Update(model);
        }

        public void Save()
        {
            this.unitOfWork.Save();
        }

        public T GetById<T>(int? id) where T : class
        {
            return this.unitOfWork.GetRepository<T>().GetByID(id);
        }

        public T GetById<T>(object[] ids) where T : class
        {
            return this.unitOfWork.GetRepository<T>().GetByID(ids);
        }

        public void Delete<T>(T model) where T : class
        {
            this.unitOfWork.GetRepository<T>().Delete(model);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool isDisposing)
        {
            if (!this.isDisposed)
            {
                if (isDisposing)
                {
                    this.unitOfWork.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}