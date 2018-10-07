using Formulario.ComplexProperties;
using Formulario.DTO;
using Formulario.Formulario;
using Formulario.Perguntas;

namespace Formulario.Respostas
{
    public abstract class Resposta
    {
        public long RespostaID { get; set; }
        public long PerguntaID { get; set; }
        public virtual Pergunta Pergunta { get; set; }
        public long RespostaModeloFormularioID { get; set; }
        public virtual RespostaModeloDeFormulario RespostaModeloDeFormulario { get; set; }

        public ControleUsuario ControleAtualizacao { get; set; }

        public virtual void AtribuirResposta(RespostaDTO resposta)
        {
            this.PerguntaID = resposta.PerguntaID;            
        }

        public abstract bool Validar();
    }
}
