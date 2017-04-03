using MicroLite;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace WebAppMicroLiteORM.Models
{
    public sealed class Repository<T> : IRepository<T>
        where T : class, new()
    {
        private IConnect _connect;
        public Repository(IConnect connect)
        {
            _connect = connect;
        }
        public async Task<bool> DeleteAsync(T model)
        {
            return await _connect.DeleteAsync(model);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            T _model = await FindAsync(id);
            return await DeleteAsync(_model);
        }

        public async Task<T> FindAsync(object id)
        {
            return await _connect.FindAsync<T>(id);
        }

        public async Task<T> InsertAsync(T model)
        {
            return await _connect.InsertAsync(model);
        }

        public async Task<IList<T>> ListAsync(SqlQuery query)
        {
            return await _connect.ListAsync<T>(query);
        }

        public async Task<PagedResult<T>> PageAsync(SqlQuery query, int page, int total)
        {
            return await _connect.PageAsync<T>(query, page, total);
        }

        public async Task<bool> UpdateAsync(T model)
        {
            return await _connect.UpdateAsync(model);
        }
    }
}