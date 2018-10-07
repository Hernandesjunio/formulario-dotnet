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
    public class PerguntaCondicionalAnexo : PerguntaCondicional
    {        
        public int ValorAtivacao { get; set; }

        public override void AtribuirCondicional(PerguntaCondicionalDTO pCondicional, Pergunta pergunta)
        {
            base.AtribuirCondicional(pCondicional, pergunta);

            Pergunta = pergunta;            
            PerguntaID = pCondicional.PerguntaID;
            ValorAtivacao = Convert.ToInt32(pCondicional.ValorAtivacao);
        }

        public override bool ValidarCondicional(object resp)
        {
            var resposta = (RespostaAnexo)resp;
            var condicao = false;
            var length = resposta.Valor.Conteudo.Length;

            switch (Operacao)
            {
                case eOperacaoCondicional.Anexo_Maior:
                    condicao = length > ValorAtivacao;
                    break;
                case eOperacaoCondicional.Anexo_Menor:
                    condicao = length < ValorAtivacao;
                    break;
                case eOperacaoCondicional.Anexo_MaiorIgual:
                    condicao = length >= ValorAtivacao;
                    break;
                case eOperacaoCondicional.Anexo_MenorIgual:
                    condicao = length <= ValorAtivacao;
                    break;
            }

            return condicao;
        }
    }
}
