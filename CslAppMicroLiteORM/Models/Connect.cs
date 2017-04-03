using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroLite;
using MicroLite.Configuration;

namespace CslAppMicroLiteORM.Models
{
    public class Connect : IConnect
    {
        public ISessionFactory SessionFactory { get; private set; }
        public ISession Session { get; private set; }
        public IAsyncSession SessionAsync { get; private set; }

        public Connect()
        {
            Initialize();    
        }

        #region Initialize
        
        protected void Initialize()
        {
            if (SessionFactory == null)
            {
                Configure
                    .Extensions()
                    .WithAttributeBasedMapping();

                SessionFactory = Configure                        
                        .Fluently()                        
                        .ForMsSql2012Connection("DB")
                        .CreateSessionFactory();
            }
            if (Session == null)
            {
                Session = SessionFactory.OpenSession();
            }
            if (SessionAsync == null)
            {
                SessionAsync = SessionFactory.OpenAsyncSession();                
            }
        }

        #endregion

        #region Dispose
                
        public void Dispose()
        {
            if (Session != null)
            {
                Session.Dispose();
                Session = null;
            }

            if (SessionAsync != null)
            {
                SessionAsync.Dispose();
                SessionAsync = null;
            }

            if (SessionFactory != null)
            {
                SessionFactory = null;
            }
        }

        #endregion

        #region NoAsyncMethods

        public T Insert<T>(T model)
        {
            ITransaction trans = Session.BeginTransaction();
            try
            {                
                Session.Insert(model);
                trans.Commit();
                return model;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }  
            finally
            {
                trans.Dispose();
            }
        }

        public bool Update<T>(T model)
        {
            ITransaction trans = Session.BeginTransaction();
            try
            {
                bool result = Session.Update(model);
                trans.Commit();
                return result;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                trans.Dispose();
            }
        }

        public bool Delete<T>(T model)
        {
            ITransaction trans = Session.BeginTransaction();
            try
            {
                bool result = Session.Delete(model);
                trans.Commit();
                return result;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                trans.Dispose();
            }
        }

        public bool Delete<T>(object id) where T : class, new()
        {
            T model = Find<T>(id);
            if (model != null)
            {
                return Delete<T>(model);
            }
            return false;
        }

        public T Find<T>(object id) where T : class, new()
        {
            return Session.Single<T>(id);
        }

        public PagedResult<T> Page<T>(SqlQuery query, int page, int total) where T : class, new()
        {
            return Session.Paged<T>(query, PagingOptions.ForPage(page, total));
        }

        public IList<T> List<T>(SqlQuery query) where T : class, new()
        {
            return Session.Fetch<T>(query);
        }        

        #endregion

        #region AsyncMethods
        public async Task<T> InsertAsync<T>(T model)
        {
            ITransaction trans = Session.BeginTransaction();
            try
            {
                await SessionAsync.InsertAsync(model);
                trans.Commit();
                return model;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                trans.Dispose();
            }
        }

        public async Task<bool> UpdateAsync<T>(T model)
        {
            ITransaction trans = Session.BeginTransaction();
            try
            {
                bool result = await SessionAsync.UpdateAsync(model);
                trans.Commit();
                return result;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                trans.Dispose();
            }
        }

        public async Task<bool> DeleteAsync<T>(T model)
        {
            ITransaction trans = Session.BeginTransaction();
            try
            {
                bool result = await SessionAsync.DeleteAsync(model);
                trans.Commit();
                return result;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                trans.Dispose();
            }
        }

        public async Task<T> FindAsync<T>(object id) where T : class, new()
        {
            return await SessionAsync.SingleAsync<T>(id);
        }

        public async Task<PagedResult<T>> PageAsync<T>(SqlQuery query, int page, int total) where T : class, new()
        {
            return await SessionAsync.PagedAsync<T>(query, PagingOptions.ForPage(page, total));
        }

        public async Task<IList<T>> ListAsync<T>(SqlQuery query) where T : class, new()
        {
            return await SessionAsync.FetchAsync<T>(query);
        }       

        public async Task<bool> DeleteAsync<T>(object id) where T : class, new()
        {
            T model = await FindAsync<T>(id);
            if (model != null)
            {
                return await DeleteAsync(model);
            }
            return false;
        }
        #endregion
    }
}
