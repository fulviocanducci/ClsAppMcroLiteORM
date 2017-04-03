using MicroLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAppMicroLiteORM.Models
{
    public interface IRepository<T>
        where T: class, new()
    {
        Task<T> InsertAsync(T model);
        Task<bool> UpdateAsync(T model);
        Task<bool> DeleteAsync(T model);
        Task<bool> DeleteAsync(object id);
        Task<T> FindAsync(object id);
        Task<PagedResult<T>> PageAsync(SqlQuery query, int page, int total);
        Task<IList<T>> ListAsync(SqlQuery query);
    }    
}
