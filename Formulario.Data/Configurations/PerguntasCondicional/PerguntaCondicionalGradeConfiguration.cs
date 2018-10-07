using Formulario.Business.Perguntas.Concicional;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.PerguntasCondicional
{
    public class PerguntaCondicionalGradeConfiguration : EntityTypeConfiguration<PerguntaCondicionalGrade>
    {
        public PerguntaCondicionalGradeConfiguration()
        {
            //Property(c => c.ValorAtivacao).IsRequired().HasColumnName("ValorAtivacaoGrade");
            Property(c => c.Operacao).IsRequired().HasColumnName("OperacaoID");
            HasMany(c => c.OpcoesAtivacao)
                .WithMany();
        }
    }
}
