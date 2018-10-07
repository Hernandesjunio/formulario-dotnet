using Formulario;
using Formulario.Business.Perguntas;
using System;
using Formulario.Business.DTO;

namespace Formulario.Business.Respostas
{
    public class RespostaNumero : Respostas.Resposta
    {        
        public decimal? Valor { get; set; }

        public override Resposta AtribuirResposta(RespostaDTO resposta)
        {
            base.AtribuirResposta(resposta);

            if (resposta.Valor != null)
                Valor = Convert.ToDecimal(resposta.Valor);

            return this;
        }

        public override bool Validar()
        {
            var pNumero = Pergunta as PerguntaNumero;

            if (pNumero.Obrigatorio && Valor.HasValue == false)
                return false;

            if (Valor.HasValue)
            {
                if (pNumero.TipoValidadorID == eTipoValidador.Numero_MaiorIgualZero && Valor < 0)
                    return false;

                if (pNumero.TipoValidadorID == eTipoValidador.Numero_MaiorZero && Valor <= 0)
                    return false;

                if (pNumero.TipoValidadorID == eTipoValidador.Numero_MenorIgualZero && Valor > 0)
                    return false;

                if (pNumero.TipoValidadorID == eTipoValidador.Numero_MenorZero && Valor >= 0)
                    return false;
            }

            return true;
        }
    }
}
