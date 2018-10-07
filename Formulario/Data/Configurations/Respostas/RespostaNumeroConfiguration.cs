using Formulario.Respostas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Respostas
{
    public class RespostaNumeroConfiguration : EntityTypeConfiguration<RespostaNumero>
    {
        public RespostaNumeroConfiguration()
        {
            Property(c => c.Valor).HasColumnName("ValorNumero");
        }
    }
}
