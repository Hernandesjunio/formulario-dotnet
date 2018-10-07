using Formulario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.Business.DTO;
using Formulario.Business.Respostas;

namespace Formulario.Business.Perguntas.Concicional
{
    public class PerguntaCondicionalNumero : PerguntaCondicional
    {        
        public decimal ValorAtivacao { get; set; }
        public override PerguntaCondicional AtribuirCondicional(PerguntaCondicionalDTO pCondicional, Pergunta pergunta)
        {
            base.AtribuirCondicional(pCondicional, pergunta);

            Pergunta = pergunta;
            Operacao = (eOperacaoCondicional)pCondicional.OperacaoCondicional.Value;
            PerguntaID = pCondicional.PerguntaID;
            ValorAtivacao = Convert.ToDecimal(pCondicional.ValorAtivacao);
            return this;
        }
        public override bool VerificarAtivacaoCondicional(Resposta resp)
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
