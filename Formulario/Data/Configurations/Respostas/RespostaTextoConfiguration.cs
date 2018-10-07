using Formulario.Respostas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Respostas
{
    public class RespostaTextoConfiguration : EntityTypeConfiguration<RespostaTexto>
    {
        public RespostaTextoConfiguration()
        {
            Property(c => c.Valor).HasColumnName("ValorTexto");
        }
    }
}
