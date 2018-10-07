using Formulario.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.DTO;

namespace Formulario.Perguntas.Concicional
{
    public class PerguntaCondicionalNumero : PerguntaCondicional
    {        
        public decimal ValorAtivacao { get; set; }
        public override void AtribuirCondicional(PerguntaCondicionalDTO pCondicional, Pergunta pergunta)
        {
            Pergunta = pergunta;
            Operacao = (eOperacaoCondicional)pCondicional.OperacaoCondicional.Value;
            PerguntaID = pCondicional.PerguntaID;
            ValorAtivacao = Convert.ToInt32(pCondicional.ValorAtivacao.ToString());
        }
        public override bool ValidarCondicional(object resp)
        {
            var resposta = (Respostas.RespostaNumero)resp;
            var condicao = false;

            switch (Operacao)
            {
                case eOperacaoCondicional.Numero_Igual:
                    condicao = ValorAtivacao == resposta.Valor;
                    break;
                case eOperacaoCondicional.Numero_Diferente:
                    condicao = ValorAtivacao != resposta.Valor;
                    break;
                case eOperacaoCondicional.Numero_Maior:
                    condicao = resposta.Valor > ValorAtivacao;
                    break;
                case eOperacaoCondicional.Numero_Menor:
                    condicao = resposta.Valor < ValorAtivacao;
                    break;
                case eOperacaoCondicional.Numero_MaiorIgual:
                    condicao = resposta.Valor >= ValorAtivacao;
                    break;
                case eOperacaoCondicional.Numero_MenorIgual:
                    condicao = resposta.Valor <= ValorAtivacao;
                    break;
            }

            return condicao;
        }
    }
}
