using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.ComplexProperties
{
    public class ControleUsuario
    {
        public string UsuarioID { get; set; }
        public DateTime Data { get; set; }

        internal static ControleUsuario Criar(string usuarioID)
        {
            return new ControleUsuario { Data = DateTime.Now, UsuarioID = usuarioID };
        }

        internal static ControleUsuario Criar(object usuarioID)
        {
            throw new NotImplementedException();
        }
    }
}
