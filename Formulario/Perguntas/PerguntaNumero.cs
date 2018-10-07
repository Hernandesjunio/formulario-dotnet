using System;
using Formulario.DTO;
using Formulario.Enumeradores;

namespace Formulario.Perguntas
{
    public class PerguntaNumero : Pergunta
    {
        public string Prefixo { get; set; }
        public byte CasasDecimais { get; set; } = 0;
        public string Sufixo { get; set; }
        //public eTipoEntradaNumero TipoEntrada { get; set; } = eTipoEntradaNumero.CaixaDeTexto;
        //public eValidadorRespostaNumero Validador { get; set; } = eValidadorRespostaNumero.QualquerValor;

        public override void AtribuirPergunta(PerguntaDTO perguntaDTO)
        {
            base.AtribuirPergunta(perguntaDTO);

            this.Prefixo = perguntaDTO.Prefixo;
            this.CasasDecimais = perguntaDTO.CasasDecimais ?? 0;
            this.Sufixo = perguntaDTO.Sufixo;
            //this.TipoEntrada = (eTipoEntradaNumero)perguntaDTO.TipoEntrada;
            this.Titulo = perguntaDTO.Titulo;
        }
    }
}
