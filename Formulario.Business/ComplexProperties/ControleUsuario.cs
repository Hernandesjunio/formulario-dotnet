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

        public static ControleUsuario Criar(string usuarioID)
        {
            return new ControleUsuario { Data = DateTime.Now, UsuarioID = usuarioID };
        }                
    }
}
