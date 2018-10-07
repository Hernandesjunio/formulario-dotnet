using System;
using Formulario.Business.DTO;
using Formulario;

namespace Formulario.Business.Perguntas
{
    public class PerguntaAnexo : Pergunta
    {
        public int TamanhoMaximoBytes { get; set; } = 5242880;
                
        public override Pergunta AtribuirPergunta(PerguntaDTO perguntaDTO)
        {
            base.AtribuirPergunta(perguntaDTO);
            this.TipoValidadorID = null;
            this.TamanhoMaximoBytes = perguntaDTO.TamanhoMaximoBytes.Value;

            return this;
        }
    }
}
