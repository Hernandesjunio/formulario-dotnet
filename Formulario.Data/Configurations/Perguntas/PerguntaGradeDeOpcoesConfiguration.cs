using Formulario.Business.Perguntas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Perguntas
{
    public class PerguntaGradeDeOpcoesConfiguration : EntityTypeConfiguration<PerguntaGradeDeOpcoes>
    {
        public PerguntaGradeDeOpcoesConfiguration()
        {
            HasMany(d => d.Linhas)
                .WithRequired(c => c.GradeOpcoes)
                .HasForeignKey(c => c.PerguntaGradeID);
                       
        }
    }
}
