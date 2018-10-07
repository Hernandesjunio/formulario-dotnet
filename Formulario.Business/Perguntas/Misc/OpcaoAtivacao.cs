using Formulario.ComplexProperties;
using Formulario;
using Formulario.Business.Perguntas.Concicional;
using Formulario.Business.Perguntas.Misc;
using Formulario.Business.Respostas;
using System.Collections.Generic;
using System.Linq;

namespace Formulario.Business.Perguntas.Misc
{
    public class OpcaoAtivacao
    {
        public long PerguntaCondicionalID { get; set; }
        public virtual PerguntaCondicionalOpcoesMultipla PerguntaCondicional { get; set; }
        public long OpcaoID { get; set; }
        public virtual Opcao Opcao { get; set; }
        public ControleUsuario ControleAtualizacao { get; set; }
    }
}
