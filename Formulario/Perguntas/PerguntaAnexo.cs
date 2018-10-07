using System;
using Formulario.DTO;
using Formulario.Enumeradores;

namespace Formulario.Perguntas
{
    public class PerguntaAnexo : Pergunta
    {
        public int TamanhoMaximoBytes { get; set; }
        //public eTipoEntradaAnexo TipoEntrada { get; set; }

        public override void AtribuirPergunta(PerguntaDTO perguntaDTO)
        {
            base.AtribuirPergunta(perguntaDTO);

            this.TamanhoMaximoBytes = perguntaDTO.TamanhoMaximoBytes.Value;            
        }
    }
}
