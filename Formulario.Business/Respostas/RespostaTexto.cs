using Formulario;
using Formulario.Business.Perguntas;
using System;
using System.Text.RegularExpressions;
using Formulario.Business.DTO;

namespace Formulario.Business.Respostas
{
    public class RespostaTexto : Resposta
    {
        public string Valor { get; set; }

        public override Resposta AtribuirResposta(RespostaDTO resposta)
        {
            base.AtribuirResposta(resposta);

            if (resposta.Valor != null)
                Valor = resposta.Valor.ToString();

            return this;
        }

        public override bool Validar()
        {
            var pText = this.Pergunta as PerguntaTexto;

            if (pText.Obrigatorio && (string.IsNullOrEmpty(Valor) || Valor.Trim().Length == 0))
                return false;

            if (string.IsNullOrEmpty(Valor) && Valor.Length > pText.TamanhoMaximo)
                return false;

            if (!string.IsNullOrEmpty(Valor))
            {
                if (pText.TipoValidadorID == eTipoValidador.Texto_Regex && Regex.IsMatch(Valor, pText.PatternRegex) == false)
                    return false;
                else if (pText.TipoValidadorID == eTipoValidador.Texto_CNPJ && Validators.CNPJ.IsCnpj(Valor) == false)
                    return false;
                else if (pText.TipoValidadorID == eTipoValidador.Texto_CPF && Validators.CPF.IsCpf(Valor) == false)
                    return false;
                else if (pText.TipoValidadorID == eTipoValidador.Texto_Email && Regex.IsMatch(Valor, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$") == false)
                    return false;
            }

            return true;
        }
    }
}
