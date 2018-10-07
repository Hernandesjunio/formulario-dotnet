using System;
using Formulario.Business.DTO;
using Formulario;

namespace Formulario.Business.Perguntas
{        
    public class PerguntaEscolhaUnica : PerguntaComOpcoes
    {        
        //public eTipoEntradaEscolhaUnica TipoEntrada { get; set; }

        public override Pergunta AtribuirPergunta(PerguntaDTO perguntaDTO)
        {
            base.AtribuirPergunta(perguntaDTO);
            return this;         
        }
    }
}
