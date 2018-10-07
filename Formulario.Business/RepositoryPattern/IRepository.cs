using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Business.RepositoryPattern
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T Create();
        T Insert(T update);
        void Update(T obj);
        void Delete(T obj);
        void Detach(T obj);
        IQueryable<T> GetQuery(bool TrackingChanges = true);
    }
}
