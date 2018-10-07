using Formulario.Respostas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Respostas
{
    public class RespostaGradeConfiguration : EntityTypeConfiguration<RespostaGrade>
    {
        public RespostaGradeConfiguration()
        {
            HasMany(c => c.Respostas)
                .WithRequired(c => c.RespostaGrade)
                .HasForeignKey(c => c.RespostaGradeID);
        }
    }
}
