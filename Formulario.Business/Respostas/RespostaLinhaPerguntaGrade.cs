using Formulario.Business.Perguntas;
using System.Collections.Generic;
using System;
using System.Linq;
using Formulario.Business.Perguntas.Misc;
using Formulario.Business.DTO;

namespace Formulario.Business.Respostas
{
    public class RespostaLinhaPerguntaGrade
    {
        public long RespostaGradeID { get; set; }
        public virtual RespostaGrade RespostaGrade { get; set; }
        public long? OpcaoRespondidaID { get; set; }
        public virtual Opcao OpcaoRespondida { get; set; }
        public long LinhaPerguntaGradeID { get; set; }
        public virtual LinhaPerguntaGrade LinhaPerguntaGrade { get; set; }
    }
}
