using System;
using Formulario.DTO;
using Formulario.Perguntas;
using Formulario.Perguntas.Misc;

namespace Formulario.Respostas
{
    public class RespostaUnica : Resposta
    {
        public long? OpcaoEscolhidaID { get; set; }
        public virtual Opcao OpcaoEscolhida { get; set; }

        public override void AtribuirResposta(RespostaDTO resposta)
        {
            base.AtribuirResposta(resposta);

            if (resposta.OpcaoID != null && resposta.OpcaoID.HasValue)
                OpcaoEscolhidaID = resposta.OpcaoID.Value;
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
