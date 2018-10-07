using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Business.RepositoryPattern
{
    public interface IDatabaseFactory: IDisposable
    {
        IDatabaseContext Get();
    }
}
