using Formulario.ComplexProperties;
using Formulario.Enumeradores;
using Formulario.Perguntas.Misc;
using Formulario.Respostas;
using System.Collections.Generic;
using System.Linq;

namespace Formulario.Perguntas.Concicional
{    
    public abstract class PerguntaCondicionalOpcoesMultipla : PerguntaCondicionalOpcoes
    {        

        public ICollection<OpcaoAtivacao> OpcoesAtivacao { get; set; }

        public override bool ValidarCondicional(object resposta)
        {
            var obj = (RespostaMultipla)resposta;
            var opcoes = OpcoesAtivacao.Select(d => d.OpcaoID).ToList();
            bool condicao = false;
            switch (Operacao)
            {
                //Pelo menos um marcado
                case eOperacaoCondicional.MultiplaOpcoes_Contem:
                    condicao = obj.OpcoesEscolhida.Any(d => opcoes.Contains(d.OpcaoID));
                    break;
                //Não contém nenhum marcado
                case eOperacaoCondicional.MultiplaOpcoes_NaoContem:
                    condicao = obj.OpcoesEscolhida.Any(d => opcoes.Contains(d.OpcaoID)) == false;
                    break;
                //Não contenha elementos no esperado e escolhido ou elementos iguais
                case eOperacaoCondicional.MultiplaOpcoes_Igual:
                    condicao = obj.OpcoesEscolhida.Any() == false && opcoes.Any() == false ||
                               obj.OpcoesEscolhida.Select(d => d.OpcaoID).OrderBy(d => d).ToList().Equals(opcoes);
                    break;
                default:
                    break;
            }

            return condicao;
        }
    }
}
