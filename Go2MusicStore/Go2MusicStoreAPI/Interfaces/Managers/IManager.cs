namespace Go2MusicStore.API.Interfaces.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IManager
    {
        void Add<T>(T model) where T : class;

        IEnumerable<T> Get<T>() where T : class;

        IEnumerable<T> Get<T>(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "") where T : class;


        void Update<T>(T model) where T : class;

        void Save();

        T GetById<T>(int? id) where T : class;

        T GetById<T>(object[] ids) where T : class;

        void Delete<T>(T genre) where T : class;
    }
}