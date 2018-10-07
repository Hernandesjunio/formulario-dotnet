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
    public class PerguntaCondicionalTexto : PerguntaCondicional
    {        
        public string ValorAtivacao { get; set; }

        public override PerguntaCondicional AtribuirCondicional(PerguntaCondicionalDTO pCondicional, Pergunta pergunta)
        {
            base.AtribuirCondicional(pCondicional, pergunta);

            Pergunta = pergunta;
            Operacao = (eOperacaoCondicional)pCondicional.OperacaoCondicional.Value;
            PerguntaID = pCondicional.PerguntaID;
            ValorAtivacao = pCondicional.ValorAtivacao.ToString();
            return this;
        }

        public override bool VerificarAtivacaoCondicional(Resposta resp)
        {
            var resposta = (RespostaTexto)resp;
            var condicao = false;

            switch (Operacao)
            {
                case eOperacaoCondicional.Texto_Igual:
                    condicao = resposta.Valor == ValorAtivacao;
                    break;
                case eOperacaoCondicional.Texto_Diferente:
                    condicao = resposta.Valor != ValorAtivacao;
                    break;
                case eOperacaoCondicional.Texto_Contem:
                    condicao = resposta.Valor != null && resposta.Valor.Contains(ValorAtivacao);
                    break;
                case eOperacaoCondicional.Texto_NaoContem:
                    condicao = resposta.Valor != null && resposta.Valor.Contains(ValorAtivacao) == false;
                    break;
            }

            return condicao;
        }
    }
}
