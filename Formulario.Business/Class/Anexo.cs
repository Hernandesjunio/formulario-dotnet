using Formulario.ComplexProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Business.Class
{
    public abstract class Anexo
    {
        public long AnexoID { get; set; }
        public Guid UniqueIdentifier { get; set; } = Guid.NewGuid();
        public string Nome { get; set; }
        public string Extensao { get; set; }
        public byte[] Conteudo { get; set; }
        public string Checksum { get; set; }
        public ControleUsuario ControleUsuario { get; set; }
    }
}
