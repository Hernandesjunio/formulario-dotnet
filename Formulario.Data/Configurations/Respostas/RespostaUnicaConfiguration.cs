using Formulario.Business.Respostas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Respostas
{
    public class RespostaUnicaConfiguration : EntityTypeConfiguration<RespostaUnica>
    {
        public RespostaUnicaConfiguration()
        {
            HasOptional(c => c.OpcaoEscolhida)
                .WithMany()
                .HasForeignKey(c => c.OpcaoEscolhidaID)
                .WillCascadeOnDelete(false);
        }
    }
}
