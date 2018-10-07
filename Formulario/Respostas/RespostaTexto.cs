using Formulario.Enumeradores;
using Formulario.Perguntas;
using System;
using System.Text.RegularExpressions;
using Formulario.DTO;

namespace Formulario.Respostas
{
    public class RespostaTexto : Resposta
    {
        public string Valor { get; set; }

        public override void AtribuirResposta(RespostaDTO resposta)
        {
            base.AtribuirResposta(resposta);

            if (resposta.Valor != null)
                Valor = resposta.Valor.ToString();
        }

        public override bool Validar()
        {
            var pText = this.Pergunta as PerguntaTexto;

            if (pText.Obrigatorio && string.IsNullOrEmpty(Valor))
                return false;

            if (string.IsNullOrEmpty(Valor) && Valor.Length > pText.TamanhoMaximo)
                return false;

            if (pText.TipoValidadorID == eTipoValidador.Texto_Regex && Regex.IsMatch(Valor, pText.PatternRegex) == false)
                return false;

            return true;
        }
    }
}
