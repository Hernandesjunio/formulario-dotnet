using System;
using Formulario.Business.DTO;

namespace Formulario.Business.Perguntas.Misc
{
    public class LinhaPerguntaGrade
    {
        public long LinhaPerguntaGradeID { get; set; }
        public string Titulo { get; set; }
        public long PerguntaGradeID { get; set; }
        public virtual PerguntaGradeDeOpcoes GradeOpcoes { get; set; }
                
    }
}
