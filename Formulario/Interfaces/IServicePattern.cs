using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Interfaces
{
    public interface IServicePattern
    {
        int Commit(string UserID);
        void Rollback();
    }
}
