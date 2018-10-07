using System;
using Formulario.Business.DTO;
using Formulario;
using Formulario.Business.Class;

namespace Formulario.Business.Perguntas
{
    public class PerguntaMultiplaEscolha : PerguntaComOpcoes
    {        
        public override Pergunta AtribuirPergunta(PerguntaDTO perguntaDTO)
        {
            base.AtribuirPergunta(perguntaDTO);
            return this;
        }
    }
}
