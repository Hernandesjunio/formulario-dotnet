using Formulario.ComplexProperties;
using Formulario.Business.DTO;
using Formulario.Business;
using Formulario.Business.Perguntas;

namespace Formulario.Business.Respostas
{
    public abstract class Resposta
    {
        public long RespostaID { get; set; }
        public long PerguntaID { get; set; }
        public virtual Pergunta Pergunta { get; set; }
        public long RespostaModeloFormularioID { get; set; }
        public virtual RespostaModeloDeFormulario RespostaModeloDeFormulario { get; set; }

        public ControleUsuario ControleAtualizacao { get; set; }

        public virtual Resposta AtribuirResposta(RespostaDTO resposta)
        {
            this.PerguntaID = resposta.PerguntaID;
            return this;
        }

        public abstract bool Validar();
    }
}
