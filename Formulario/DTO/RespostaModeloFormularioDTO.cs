using Formulario.Leiaute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.DTO
{
    public class RespostaModeloFormularioDTO
    {
        /// <summary>
        /// Modelo de formulário composto de suas perguntas
        /// </summary>
        public ModeloFormularioDTO ModeloFormulario { get; set; }

        public long RespostaModeloFormularioID { get; set; }

        /// <summary>
        /// Respostas das perguntas do modelo de formulário DTO
        /// </summary>
        public List<RespostaDTO> Respostas { get; set; }
    }
}
