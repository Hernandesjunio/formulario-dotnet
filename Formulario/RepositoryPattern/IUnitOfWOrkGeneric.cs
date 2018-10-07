using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.RepositoryPattern
{
    public interface IUnitOfWOrkGeneric<T> where T : IDatabaseFactory
    {
        Task<int> CommitAsync(string UserID);
        int Commit(string UserID);
        void Rollback();
    }
}
