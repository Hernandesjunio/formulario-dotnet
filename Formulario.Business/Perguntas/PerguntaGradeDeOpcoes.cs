using Formulario.Business.Perguntas.Misc;
using System.Collections.Generic;
using Formulario.Business.DTO;
using System;
using System.Linq;

namespace Formulario.Business.Perguntas
{

    public class PerguntaGradeDeOpcoes : PerguntaComOpcoes
    {
        public ICollection<LinhaPerguntaGrade> Linhas { get; set; } = new HashSet<LinhaPerguntaGrade>();
        //public ICollection<Opcao> Opcoes { get; set; }

        public override Pergunta AtribuirPergunta(PerguntaDTO perguntaDTO)
        {
            base.AtribuirPergunta(perguntaDTO);

            var linhas = perguntaDTO.LinhasGrade.Where(d => Linhas.Any(c => c.LinhaPerguntaGradeID == d.LinhaID) == false).ToList();

            foreach (var item in Linhas.ToList())
            {
                if (perguntaDTO.LinhasGrade.ContainsKey(item.LinhaPerguntaGradeID))//update
                    item.Titulo = perguntaDTO.LinhasGrade.Find(item.LinhaPerguntaGradeID).Descricao;
                else //delete
                    Linhas.Remove(item);
            }

            foreach (var item in linhas)
            {//insert
                this.Linhas.Add(new LinhaPerguntaGrade { LinhaPerguntaGradeID = item.LinhaID, Titulo = item.Descricao });
            }

            return this;
        }
    }
}
