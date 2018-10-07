using Formulario;
using Formulario.Business.Perguntas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.Business.DTO;

namespace Formulario.Business.Respostas
{
    public class RespostaData : Respostas.Resposta
    {
        public DateTime? Valor { get; set; }

        public override Resposta AtribuirResposta(RespostaDTO resposta)
        {
            base.AtribuirResposta(resposta);

            if (resposta.Valor != null)
                Valor = Convert.ToDateTime(resposta.Valor);

            return this;
        }

        public override bool Validar()
        {
            var pDateTime = Pergunta as PerguntaData;

            if (pDateTime.Obrigatorio && Valor.HasValue == false)
                return false;

            var hoje = DateTime.Now.Date;
            Valor = Valor?.Date;

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
