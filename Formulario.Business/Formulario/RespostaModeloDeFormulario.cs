using Formulario.ComplexProperties;
using Formulario.Business.Perguntas;
using Formulario.Business.Respostas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Formulario.Business
{
    public class RespostaModeloDeFormulario
    {
        public long RespostaModeloFormularioID { get; set; }
        public long ModeloDeFormularioID { get; set; }
        public virtual ModeloDeFormulario ModeloDeFormulario { get; set; }
        public virtual ICollection<Resposta> Respostas { get; set; } = new HashSet<Resposta>();
        public ControleUsuario ControleAtualizacao { get; set; }

        public void Validar()
        {
            foreach (var pergunta in ModeloDeFormulario.Perguntas)
            {
                var resposta = Respostas.SingleOrDefault(d => d.Pergunta.PerguntaID == pergunta.PerguntaID);

                if (resposta == null || pergunta.PerguntaCondicional != null)
                {
                    if (pergunta.PerguntaCondicional != null)
                    {
                        var respostaOrigemCondicional = Respostas.SingleOrDefault(d => d.Pergunta.PerguntaID == pergunta.PerguntaCondicional.Pergunta.PerguntaID);

                        if (respostaOrigemCondicional == null)
                            throw new ApplicationException($"Não foi possível localizar a resposta para a pergunta condicional {pergunta.PerguntaCondicional.PerguntaID} {pergunta.PerguntaCondicional.Pergunta.Titulo}");

                        var validacaoCondicional = pergunta.PerguntaCondicional.VerificarAtivacaoCondicional(respostaOrigemCondicional);

                        if (resposta == null && validacaoCondicional == true)
                            throw new ApplicationException($"Não há resposta para a pergunta condicional {pergunta.Titulo}");
                        else if (resposta != null && validacaoCondicional == false)
                            throw new ApplicationException($"A pergunta condicional {resposta.Pergunta.PerguntaCondicional.Pergunta.Titulo} possui o valor incorreto");
                    }
                    else
                        throw new ApplicationException($"Não há resposta para a pergunta {pergunta.Titulo}");

                }

                if (resposta != null && resposta.Validar() == false)
                    throw new ApplicationException($"A pergunta {resposta.Pergunta.Titulo} possui o valor incorreto");
            }
        }
    }
}
