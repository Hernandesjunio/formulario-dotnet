using System;
using Formulario.Business.DTO;
using Formulario;

namespace Formulario.Business.Perguntas
{
    public class PerguntaTexto : Pergunta
    {
        public short TamanhoMaximo { get; set; } = 50;
        //public eValidadorTexto Validador { get; set; } = eValidadorTexto.NaoValidar;
        //public eTipoEntradaTexto TipoEntrada { get; set; } = eTipoEntradaTexto.CaixaDeTexto;
        public string PatternRegex { get; set; }

        public override Pergunta AtribuirPergunta(PerguntaDTO perguntaDTO)
        {
            base.AtribuirPergunta(perguntaDTO);

            this.PatternRegex = perguntaDTO.PatternRegex;
            this.TamanhoMaximo = perguntaDTO.TamanhoMaximo.Value;
            return this;
        }
    }
}
