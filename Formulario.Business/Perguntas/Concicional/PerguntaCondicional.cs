using Formulario.ComplexProperties;
using Formulario.Business.DTO;
using Formulario;
using System;
using Formulario.Business.Respostas;

namespace Formulario.Business.Perguntas.Concicional
{
    public abstract class PerguntaCondicional
    {
        public long PerguntaCondicionalID { get; set; }

        public long PerguntaID { get; set; }
        public virtual Pergunta Pergunta { get; set; }
        public eOperacaoCondicional Operacao { get; set; }

        public ControleUsuario ControleAtualizacao { get; set; }

        public abstract bool VerificarAtivacaoCondicional(Resposta resposta);

        public virtual PerguntaCondicional AtribuirCondicional(PerguntaCondicionalDTO perguntaCondicional, Pergunta pergunta)
        {            
            PerguntaCondicionalID = perguntaCondicional.PerguntaCondicionalID;
            Operacao = (eOperacaoCondicional)perguntaCondicional.OperacaoCondicional.Value;
            return this;
        }
    }       
}
