using Formulario.Business.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations
{
    public class TipoEntradaConfiguration : EntityTypeConfiguration<TipoEntrada>
    {
        public TipoEntradaConfiguration()
        {
            HasKey(c => c.TipoEntradaID);
            Property(c => c.TipoEntradaID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(c => c.Descricao).HasMaxLength(50);

            HasRequired(c => c.TipoPergunta)
                .WithMany()
                .HasForeignKey(c => c.TipoPerguntaID);
        }
    }
}
