using Formulario.Business.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Infrastructure
{
    public class DatabaseFactoryFormulario : IDatabaseFactory
    {
        protected IDatabaseContext ctx = null;

        public DatabaseFactoryFormulario()
        {

        }

        public void Dispose()
        {
            
        }

        public IDatabaseContext Get()
        {
            return ctx = ctx ?? (ctx = new FormularioContext());
        }
    }
}
