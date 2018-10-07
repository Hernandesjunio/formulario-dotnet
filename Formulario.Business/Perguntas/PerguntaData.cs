using Formulario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.Business.DTO;

namespace Formulario.Business.Perguntas
{
    public class PerguntaData : Pergunta
    {        
        public override Pergunta AtribuirPergunta(PerguntaDTO perguntaDTO)
        {
            base.AtribuirPergunta(perguntaDTO);

            return this;
        }
    }
}
