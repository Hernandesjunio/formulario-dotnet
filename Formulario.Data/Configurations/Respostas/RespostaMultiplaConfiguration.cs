using Formulario.Business.Respostas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Respostas
{
    public class RespostaMultiplaConfiguration : EntityTypeConfiguration<RespostaMultipla>
    {
        public RespostaMultiplaConfiguration()
        {
            HasMany(c => c.OpcoesEscolhida)
                .WithRequired(c => c.Resposta)
                .HasForeignKey(c => c.RespostaID);
        }
    }
}
