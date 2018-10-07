using System;
using Formulario.Business.DTO;
using Formulario;
using Formulario.Business.Perguntas.Misc;
using Formulario.Business.Respostas;
using System.Linq;

namespace Formulario.Business.Perguntas.Concicional
{
    public class PerguntaCondicionalUnica : PerguntaCondicionalOpcoes
    {        
        public virtual Opcao OpcaoAtivacao { get; set; }
        public long OpcaoAtivacaoID { get; set; }

        public override PerguntaCondicional AtribuirCondicional(PerguntaCondicionalDTO pCondicional, Pergunta pergunta)
        {
            base.AtribuirCondicional(pCondicional, pergunta);

            PerguntaID = pCondicional.PerguntaID;
            Pergunta = pergunta;
            OpcaoAtivacaoID = Convert.ToInt64(pCondicional.ValorAtivacao);
            OpcaoAtivacao = (pergunta as PerguntaEscolhaUnica).Opcoes.Single(c => c.OpcaoID == OpcaoAtivacaoID);
            return this;

        }
        public override bool VerificarAtivacaoCondicional(Resposta resposta)
        {
            var obj = (RespostaUnica)resposta;            
            bool condicao = false;
            switch (Operacao)
            {
                case eOperacaoCondicional.UnicaOpcao_Diferente:
                    condicao = obj.OpcaoEscolhidaID != OpcaoAtivacao.OpcaoID;
                    break;
                //Não contenha elementos no esperado e escolhido ou elementos iguais
                case eOperacaoCondicional.UnicaOpcao_Igual:
                    condicao = obj.OpcaoEscolhidaID == OpcaoAtivacao.OpcaoID;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return condicao;
        }
    }
}
