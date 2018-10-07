using Formulario.Business.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Infrastructure
{
    public class RepositoryGeneric<T> : IRepository<T> where T : class
    {
        private readonly DbContext db;
        DbSet<T> _ds;

        protected DbSet<T> Ds
        {
            get
            {
                return _ds ?? (_ds = db.Set<T>());
            }
        }

        public RepositoryGeneric(DbContext db)
        {
            this.db = db;
        }

        public void Delete(T obj)
        {
            Ds.Remove(obj);
        }

        public void Detach(T obj)
        {
            db.Entry(obj).State = EntityState.Detached;
        }

        public void Dispose()
        {

        }

        public IQueryable<T> GetQuery(bool TrackingChanges = true)
        {
            if (TrackingChanges == false)
                Ds.AsNoTracking().AsQueryable();

            return Ds.AsQueryable();
        }
               
        public T Create()
        {
            return Ds.Create<T>();
        }

        public T Insert(T obj)
        {
            return Ds.Add(obj);
        }

        public void Update(T obj)
        {
            Ds.Attach(obj);
        }
    }
}
