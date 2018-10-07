using Formulario.ComplexProperties;
using Formulario.DTO;
using Formulario.Enumeradores;
using System;

namespace Formulario.Perguntas.Concicional
{
    public abstract class PerguntaCondicional
    {
        public long PerguntaCondicionalID { get; set; }

        public long PerguntaID { get; set; }
        public virtual Pergunta Pergunta { get; set; }
        public eOperacaoCondicional Operacao { get; set; }

        public ControleUsuario ControleAtualizacao { get; set; }

        public abstract bool ValidarCondicional(object resposta);

        public virtual void AtribuirCondicional(PerguntaCondicionalDTO perguntaCondicional, Pergunta pergunta)
        {
            Operacao = (eOperacaoCondicional)perguntaCondicional.OperacaoCondicional.Value;
        }
    }       
}
