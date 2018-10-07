using Formulario.Business.Respostas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Respostas
{
    public class RespostaLinhaPerguntaGradeConfiguration : EntityTypeConfiguration<RespostaLinhaPerguntaGrade>
    {
        public RespostaLinhaPerguntaGradeConfiguration()
        {
            HasKey(c => new { c.RespostaGradeID, c.LinhaPerguntaGradeID });
                                    
            HasOptional(c => c.OpcaoRespondida)
                .WithMany()
                .HasForeignKey(c => c.OpcaoRespondidaID);

            HasRequired(c => c.RespostaGrade)
                .WithMany(c => c.Respostas)
                .HasForeignKey(c => c.RespostaGradeID);

            HasRequired(c => c.LinhaPerguntaGrade)
                .WithMany()
                .HasForeignKey(c => c.LinhaPerguntaGradeID);
        }
    }
}
