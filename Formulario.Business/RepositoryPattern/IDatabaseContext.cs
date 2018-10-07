using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Business.RepositoryPattern
{
    public interface IDatabaseContext: IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
    }
}
