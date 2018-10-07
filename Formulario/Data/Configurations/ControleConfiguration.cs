using Formulario.ComplexProperties;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Infrastructure
{
    public class ControleConfiguration : ComplexTypeConfiguration<ControleUsuario>
    {
        public ControleConfiguration()
        {
            Property(c => c.UsuarioID)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("UsuarioID");

            Property(c => c.Data)
                .HasColumnName("Data");
        }
    }
}
