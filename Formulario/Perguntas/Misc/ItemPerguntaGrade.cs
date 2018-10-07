using System;
using Formulario.DTO;

namespace Formulario.Perguntas.Misc
{
    public class LinhaPerguntaGrade //: Pergunta //PerguntaComOpcoes
    {
        public long LinhaPerguntaGradeID { get; set; }
        public string Titulo { get; set; }
        public long PerguntaGradeID { get; set; }
        public virtual PerguntaGradeDeOpcoes GradeOpcoes { get; set; }

        //public override void AtribuirPergunta(PerguntaDTO perguntaDTO)
        //{
        //    base.AtribuirPergunta(perguntaDTO);            
        //}
    }
}
