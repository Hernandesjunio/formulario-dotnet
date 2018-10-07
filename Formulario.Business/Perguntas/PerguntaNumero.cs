using Formulario.Business.DTO;

namespace Formulario.Business.Perguntas
{
    public class PerguntaNumero : Pergunta
    {
        public string Prefixo { get; set; }
        public byte CasasDecimais { get; set; } = 0;
        public string Sufixo { get; set; }
        
        public override Pergunta AtribuirPergunta(PerguntaDTO perguntaDTO)
        {
            base.AtribuirPergunta(perguntaDTO);

            this.Prefixo = perguntaDTO.Prefixo;
            this.CasasDecimais = perguntaDTO.CasasDecimais ?? 0;
            this.Sufixo = perguntaDTO.Sufixo;            
            this.Titulo = perguntaDTO.Titulo;
            return this;
        }
    }
}
