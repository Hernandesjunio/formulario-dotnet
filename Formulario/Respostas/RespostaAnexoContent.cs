using Formulario.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Respostas
{
    public class RespostaAnexoContent : Anexo
    {
        public virtual RespostaAnexo Resposta { get; set; }

        public RespostaAnexoContent ComputeChecksum()
        {
            Checksum = Conteudo.Checksum();
            return this;
        }
    }
}
