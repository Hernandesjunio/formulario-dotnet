using Formulario.Enumeradores;
using Formulario.Respostas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.DTO;

namespace Formulario.Perguntas.Concicional
{
    public class PerguntaCondicionalData : PerguntaCondicional
    {
        //public eOperacaoCondicionalData Operacao { get; set; }        
        public DateTime ValorAtivacao { get; set; }
        public override void AtribuirCondicional(PerguntaCondicionalDTO pCondicional, Pergunta pergunta)
        {
            Pergunta = pergunta;            
            PerguntaID = pCondicional.PerguntaID;
            ValorAtivacao = Convert.ToDateTime(pCondicional.ValorAtivacao).Date;
        }
        public override bool ValidarCondicional(object resp)
        {
            var resposta = (RespostaData)resp;
            var condicao = false;

            switch (Operacao)
            {
                case eOperacaoCondicional.Data_Igual:
                    condicao = ValorAtivacao == resposta.Valor;
                    break;
                case eOperacaoCondicional.Data_Diferente:
                    condicao = ValorAtivacao != resposta.Valor;
                    break;
                case eOperacaoCondicional.Data_Maior:
                    condicao = resposta.Valor > ValorAtivacao;
                    break;
                case eOperacaoCondicional.Data_Menor:
                    condicao = resposta.Valor < ValorAtivacao;
                    break;
                case eOperacaoCondicional.Data_MaiorIgual:
                    condicao = resposta.Valor >= ValorAtivacao;
                    break;
                case eOperacaoCondicional.Data_MenorIgual:
                    condicao = resposta.Valor <= ValorAtivacao;
                    break;
            }

            return condicao;
        }
    }
}
