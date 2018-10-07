using Formulario;
using Formulario.Business.Respostas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.Business.DTO;

namespace Formulario.Business.Perguntas.Concicional
{
    public class PerguntaCondicionalData : PerguntaCondicional
    {
        //public eOperacaoCondicionalData Operacao { get; set; }        
        public DateTime ValorAtivacao { get; set; }
        public override PerguntaCondicional AtribuirCondicional(PerguntaCondicionalDTO pCondicional, Pergunta pergunta)
        {
            base.AtribuirCondicional(pCondicional, pergunta);

            Pergunta = pergunta;            
            PerguntaID = pCondicional.PerguntaID;
            ValorAtivacao = Convert.ToDateTime(pCondicional.ValorAtivacao).Date;
            return this;
        }
        public override bool VerificarAtivacaoCondicional(Resposta resp)
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
