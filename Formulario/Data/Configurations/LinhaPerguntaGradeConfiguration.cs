using Formulario.Perguntas;
using Formulario.Perguntas.Misc;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations
{
    public class LinhaPerguntaGradeConfiguration : EntityTypeConfiguration<LinhaPerguntaGrade>
    {
        public LinhaPerguntaGradeConfiguration()
        {
            HasRequired(c => c.GradeOpcoes)
                .WithMany(c => c.Linhas)
                .HasForeignKey(c => c.PerguntaGradeID);
        }
    }
}
