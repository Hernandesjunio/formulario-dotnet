using Formulario.Business.Respostas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Respostas
{
    public class RespostaAnexoConfiguration : EntityTypeConfiguration<RespostaAnexo>
    {
        public RespostaAnexoConfiguration()
        {
            HasOptional(c => c.Valor)
                .WithMany()
                .HasForeignKey(c => c.AnexoID);
        }
    }
}
