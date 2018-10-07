using System;
using Formulario.DTO;
using Formulario.Enumeradores;
using Formulario.Perguntas.Misc;
using Formulario.Respostas;
using System.Linq;

namespace Formulario.Perguntas.Concicional
{
    public class PerguntaCondicionalUnica : PerguntaCondicionalOpcoes
    {        
        public virtual Opcao OpcaoAtivacao { get; set; }
        public long OpcaoAtivacaoID { get; set; }

        public override void AtribuirCondicional(PerguntaCondicionalDTO pCondicional, Pergunta pergunta)
        {            
            PerguntaID = pCondicional.PerguntaID;
            Pergunta = pergunta;
            OpcaoAtivacaoID = Convert.ToInt64(pCondicional.ValorAtivacao);
            OpcaoAtivacao = (pergunta as PerguntaEscolhaUnica).Opcoes.Single(c => c.OpcaoID == OpcaoAtivacaoID);

        }
        public override bool ValidarCondicional(object resposta)
        {
            var obj = (RespostaUnica)resposta;
            //var opcoes = OpcoesAtivacao.Select(d => d.OpcaoID).ToList();
            bool condicao = false;
            switch (Operacao)
            {
                case eOperacaoCondicional.UnicaOpcao_Diferente:
                    condicao = obj.OpcaoEscolhidaID == OpcaoAtivacao.OpcaoID;
                    break;
                //Não contenha elementos no esperado e escolhido ou elementos iguais
                case eOperacaoCondicional.UnicaOpcao_Igual:
                    condicao = obj.OpcaoEscolhidaID == OpcaoAtivacao.OpcaoID;
                    break;
            }

            return condicao;
        }
    }
}
