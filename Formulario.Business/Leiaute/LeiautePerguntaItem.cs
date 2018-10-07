using Formulario.ComplexProperties;
using Formulario;
using Formulario.Business.Perguntas;

namespace Formulario.Business.Leiaute
{
        
    public class LeiautePerguntaItem
    {
        public long LeiautePerguntaItemID { get; set; }
        public long LeiautePerguntaID { get; set; }

        public virtual LeiautePergunta LeiautePergunta { get; set; }

        public eTamanhoTela Responsivo { get; set; }
        public eColunas Tamanho { get; set; }

        public ControleUsuario ControleAtualizacao { get; set; }
    }
}
