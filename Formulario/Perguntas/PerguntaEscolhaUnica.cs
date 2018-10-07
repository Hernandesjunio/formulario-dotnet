using System;
using Formulario.DTO;
using Formulario.Enumeradores;

namespace Formulario.Perguntas
{        
    public class PerguntaEscolhaUnica : PerguntaComOpcoes
    {        
        //public eTipoEntradaEscolhaUnica TipoEntrada { get; set; }

        public override void AtribuirPergunta(PerguntaDTO perguntaDTO)
        {
            base.AtribuirPergunta(perguntaDTO);

            //this.TipoEntrada = (eTipoEntradaEscolhaUnica) perguntaDTO.TipoEntrada;            
        }
    }
}
