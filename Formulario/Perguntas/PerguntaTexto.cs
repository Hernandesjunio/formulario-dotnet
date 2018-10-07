using System;
using Formulario.DTO;
using Formulario.Enumeradores;

namespace Formulario.Perguntas
{
    public class PerguntaTexto : Pergunta
    {
        public short TamanhoMaximo { get; set; } = 50;
        //public eValidadorTexto Validador { get; set; } = eValidadorTexto.NaoValidar;
        //public eTipoEntradaTexto TipoEntrada { get; set; } = eTipoEntradaTexto.CaixaDeTexto;
        public string PatternRegex { get; set; }

        public override void AtribuirPergunta(PerguntaDTO perguntaDTO)
        {
            base.AtribuirPergunta(perguntaDTO);

            this.PatternRegex = perguntaDTO.PatternRegex;
            this.TamanhoMaximo = perguntaDTO.TamanhoMaximo.Value;
            //this.TipoEntrada = (eTipoEntradaTexto)perguntaDTO.TipoEntrada;
            //this.Validador = (eValidadorTexto) perguntaDTO.Validador.Value;
        }
    }
}
