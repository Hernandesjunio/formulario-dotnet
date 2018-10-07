using Formulario.Perguntas;
using System.Collections.Generic;
using System;
using Formulario.Perguntas.Misc;
using Formulario.DTO;
using System.Linq;
using Formulario.ComplexProperties;

namespace Formulario.Respostas
{
    public class RespostaMultipla : Resposta
    {
        public virtual ICollection<OpcaoRespondida> OpcoesEscolhida { get; set; }

        public override void AtribuirResposta(RespostaDTO resposta)
        {
            base.AtribuirResposta(resposta);

            OpcoesEscolhida.Clear();
            OpcoesEscolhida = resposta.Opcoes.Select(c => new OpcaoRespondida
            {
                OpcaoID = c,
                RespostaID = resposta.RespostaID,
                ControleAtualizacao = ControleUsuario.Criar(resposta.UsuarioID)
            }).ToList();
        }

        public override bool Validar()
        {
            var pMultipla = Pergunta as PerguntaMultiplaEscolha;

            if (pMultipla.Obrigatorio && OpcoesEscolhida.Count == 0)
                return false;

            return true;
        }
    }
}
