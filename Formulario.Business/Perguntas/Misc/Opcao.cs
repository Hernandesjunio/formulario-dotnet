using Formulario.ComplexProperties;

namespace Formulario.Business.Perguntas.Misc
{
    public class Opcao
    {
        public long OpcaoID { get; set; }
        public string Descricao { get; set; }

        public long PerguntaID { get; set; }
        public virtual PerguntaComOpcoes PerguntaComOpcoes { get; set; }
        public ControleUsuario ControleAtualizacao { get; set; }
    }
}
