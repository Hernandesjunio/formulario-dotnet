using Formulario.Business.Perguntas;
using System.Collections.Generic;
using System;
using Formulario.Business.Perguntas.Misc;
using Formulario.Business.DTO;
using System.Linq;
using Formulario.ComplexProperties;

namespace Formulario.Business.Respostas
{
    public class RespostaMultipla : Resposta
    {
        public virtual ICollection<OpcaoRespondida> OpcoesEscolhida { get; set; } = new HashSet<OpcaoRespondida>();

        public override Resposta AtribuirResposta(RespostaDTO resposta)
        {
            base.AtribuirResposta(resposta);

            OpcoesEscolhida.Clear();
            OpcoesEscolhida = resposta.Opcoes.Select(c => new OpcaoRespondida
            {
                OpcaoID = c,
                RespostaID = resposta.RespostaID,
                ControleAtualizacao = ControleUsuario.Criar(resposta.UsuarioID)
            }).ToList();

            return this;
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
