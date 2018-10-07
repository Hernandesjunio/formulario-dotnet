using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.RepositoryPattern
{
    public interface IDatabaseContext
    {
        IRepository<T> GetRepository<T>() where T : class;
    }
}
