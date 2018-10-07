using System;
using Formulario.DTO;
using Formulario.Enumeradores;
using Formulario.Class;

namespace Formulario.Perguntas
{
    public class PerguntaMultiplaEscolha : PerguntaComOpcoes
    {
        //public eTipoEntradaMultiplaEscolha TipoEntrada { get; set; }
        //public byte TipoEntrada { get; set; }

        //public virtual TipoEntrada TpoEntrada { get; set; }

        public override void AtribuirPergunta(PerguntaDTO perguntaDTO)
        {
            base.AtribuirPergunta(perguntaDTO);

            //this.TipoEntrada = (eTipoEntrada)perguntaDTO.TipoEntrada;
        }
    }
}
