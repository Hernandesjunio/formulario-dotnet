﻿using Formulario.Perguntas;
using System.Collections.Generic;
using System;
using Formulario.Perguntas.Misc;
using System.Linq;
using Formulario.DTO;
using Formulario.ComplexProperties;

namespace Formulario.Respostas
{
    public class RespostaGrade : Resposta
    {
        public virtual ICollection<RespostaLinhaPerguntaGrade> Respostas { get; set; } = new HashSet<RespostaLinhaPerguntaGrade>();

        public override void AtribuirResposta(RespostaDTO resposta)
        {
            base.AtribuirResposta(resposta);

            if (resposta.RespostaGrade != null)
                foreach (var item in resposta.RespostaGrade)
                {
                    var itemRespondido = Respostas.SingleOrDefault(d => d.LinhaPerguntaGradeID == item.LinhaPerguntaGradeID);
                    if (itemRespondido == null)
                    {
                        itemRespondido = new RespostaLinhaPerguntaGrade { LinhaPerguntaGradeID = item.LinhaPerguntaGradeID,
                                                                          OpcaoRespondidaID = item.OpcaoRespondidaID };
                        Respostas.Add(itemRespondido);
                    }
                    else
                    {
                        itemRespondido.OpcaoRespondidaID = item.OpcaoRespondidaID;
                        itemRespondido.LinhaPerguntaGradeID = item.LinhaPerguntaGradeID;
                    }
                }

            ControleAtualizacao = ControleUsuario.Criar(resposta.UsuarioID);
        }

        public override bool Validar()
        {
            var pGrade = Pergunta as PerguntaGradeDeOpcoes;

            var possuiAlgumaSemResposta = pGrade.Linhas.Any(d => Respostas.Any() == false || Respostas.Any(c => c.LinhaPerguntaGradeID == d.LinhaPerguntaGradeID && c.OpcaoRespondidaID.HasValue == false));

            if (pGrade.Obrigatorio && possuiAlgumaSemResposta)
                return false;

            return true;
        }
    }
}
