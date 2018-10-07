using System;
using Formulario.Business.DTO;
using Formulario.Business.Perguntas;
using Formulario.Business.Perguntas.Misc;

namespace Formulario.Business.Respostas
{
    public class RespostaUnica : Resposta
    {
        public long? OpcaoEscolhidaID { get; set; }
        public virtual Opcao OpcaoEscolhida { get; set; }

        public override Resposta AtribuirResposta(RespostaDTO resposta)
        {
            base.AtribuirResposta(resposta);

            if (resposta.OpcaoID != null && resposta.OpcaoID.HasValue)
                OpcaoEscolhidaID = resposta.OpcaoID.Value;

            return this;
        }

        public override bool Validar()
        {
            var pUnica = Pergunta as PerguntaEscolhaUnica;

            if (pUnica.Obrigatorio && OpcaoEscolhidaID.HasValue == false)
                return false;

            return true;
        }
    }
}
