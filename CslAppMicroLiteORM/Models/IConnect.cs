using MicroLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslAppMicroLiteORM.Models
{
    public interface IConnect: IDisposable
    {
        ISession Session { get; }
        ISessionFactory SessionFactory { get; }
        IAsyncSession SessionAsync { get; }

        T Insert<T>(T model);
        bool Update<T>(T model);
        bool Delete<T>(T model);
        bool Delete<T>(object id) where T : class, new();
        T Find<T>(object id) where T : class, new();
        PagedResult<T> Page<T>(SqlQuery query, int page, int total) where T : class, new();
        IList<T> List<T>(SqlQuery query) where T : class, new();

        Task<T> InsertAsync<T>(T model);
        Task<bool> UpdateAsync<T>(T model);
        Task<bool> DeleteAsync<T>(T model);
        Task<bool> DeleteAsync<T>(object id) where T : class, new();
        Task<T> FindAsync<T>(object id) where T : class, new();
        Task<PagedResult<T>> PageAsync<T>(SqlQuery query, int page, int total) where T : class, new();
        Task<IList<T>> ListAsync<T>(SqlQuery query) where T : class, new();
    }
}