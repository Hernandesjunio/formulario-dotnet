using Formulario.Enumeradores;
using Formulario.Perguntas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.DTO;

namespace Formulario.Respostas
{
    public class RespostaData : Respostas.Resposta
    {
        public DateTime? Valor { get; set; }

        public override void AtribuirResposta(RespostaDTO resposta)
        {
            base.AtribuirResposta(resposta);

            if (resposta.Valor != null)
                Valor = Convert.ToDateTime(resposta.Valor);
        }

        public override bool Validar()
        {
            var pDateTime = Pergunta as PerguntaData;

            if (pDateTime.Obrigatorio && Valor.HasValue == false)
                return false;

            var hoje = DateTime.Now.Date;

            if (pDateTime.TipoValidadorID == eTipoValidador.Data_SomenteMaiorHoje && hoje >= Valor)
                return false;

            if (pDateTime.TipoValidadorID == eTipoValidador.Data_SomenteMaiorIgualHoje && hoje > Valor)
                return false;

            if (pDateTime.TipoValidadorID == eTipoValidador.Data_SomenteMenorHoje && hoje <= Valor)
                return false;

            if (pDateTime.TipoValidadorID == eTipoValidador.Data_SomenteMenorIgualHoje && hoje < Valor)
                return false;

            return true;
        }
    }
}
