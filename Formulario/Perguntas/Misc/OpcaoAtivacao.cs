using Formulario.ComplexProperties;
using Formulario.Enumeradores;
using Formulario.Perguntas.Concicional;
using Formulario.Perguntas.Misc;
using Formulario.Respostas;
using System.Collections.Generic;
using System.Linq;

namespace Formulario.Perguntas.Misc
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
