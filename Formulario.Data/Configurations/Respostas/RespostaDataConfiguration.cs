using Formulario.Business.Respostas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Respostas
{
    public class RespostaDataConfiguration : EntityTypeConfiguration<RespostaData>
    {
        public RespostaDataConfiguration()
        {
            Property(c => c.Valor).HasColumnName("ValorData");
        }
    }
}
