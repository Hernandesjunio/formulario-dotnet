using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Business.Interfaces
{
    public interface IServicePattern: IDisposable
    {
        int Commit(string UserID);
        void Rollback();

        Task<int> CommitAsync(string UserID);
        
    }
}
