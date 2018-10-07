using Formulario.Business.Perguntas;
using System.Collections.Generic;
using System;
using Formulario.Business.Perguntas.Misc;
using System.Linq;
using Formulario.Business.DTO;
using Formulario.ComplexProperties;

namespace Formulario.Business.Respostas
{
    public class RespostaGrade : Resposta
    {
        public virtual ICollection<RespostaLinhaPerguntaGrade> Respostas { get; set; } = new HashSet<RespostaLinhaPerguntaGrade>();

        public override Resposta AtribuirResposta(RespostaDTO resposta)
        {
            base.AtribuirResposta(resposta);

            if (resposta.RespostaGrade != null)
                foreach (var item in resposta.RespostaGrade)
                {
                    var itemRespondido = Respostas.SingleOrDefault(d => d.LinhaPerguntaGradeID == item.LinhaPerguntaGradeID);
                    if (itemRespondido == null)
                    {
                        itemRespondido = new RespostaLinhaPerguntaGrade
                        {
                            LinhaPerguntaGradeID = item.LinhaPerguntaGradeID,
                            OpcaoRespondidaID = item.OpcaoRespondidaID
                        };
                        Respostas.Add(itemRespondido);
                    }
                    else
                    {
                        itemRespondido.OpcaoRespondidaID = item.OpcaoRespondidaID;
                        itemRespondido.LinhaPerguntaGradeID = item.LinhaPerguntaGradeID;
                    }
                }

            ControleAtualizacao = ControleUsuario.Criar(resposta.UsuarioID);

            return this;
        }

        public override bool Validar()
        {
            var pGrade = Pergunta as PerguntaGradeDeOpcoes;

            if (pGrade.Linhas.Any() == false)
                throw new ApplicationException($"Não há linhas para a pergunta de grade {pGrade.PerguntaID} {pGrade.Titulo}");

            if (pGrade.Linhas.Count != Respostas.Count)
                throw new ApplicationException($"Para pergunta de grade, a quantidade de respostas deverá ser igual à quantidade de linhas de grade");

            var possuiAlgumaSemResposta = pGrade.Linhas.Any(d => Respostas.Any() == false || Respostas.Any(c => c.LinhaPerguntaGradeID == d.LinhaPerguntaGradeID && c.OpcaoRespondidaID.HasValue == false));

            if (pGrade.Obrigatorio && possuiAlgumaSemResposta)
                return false;

            return true;
        }
    }
}
