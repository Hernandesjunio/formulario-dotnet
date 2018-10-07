using Formulario.Business.Perguntas.Concicional;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.PerguntasCondicional
{
    public class PerguntaCondicionalUnicaConfiguration : EntityTypeConfiguration<PerguntaCondicionalUnica>
    {
        public PerguntaCondicionalUnicaConfiguration()
        {
            Property(c => c.Operacao).IsRequired().HasColumnName("OperacaoID");

            HasRequired(c => c.OpcaoAtivacao)
                .WithMany()
                .HasForeignKey(c => c.OpcaoAtivacaoID)
                .WillCascadeOnDelete(false);
        }
    }
}
