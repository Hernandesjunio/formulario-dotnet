using Formulario.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations
{
    public class TipoValidadorConfiguration : EntityTypeConfiguration<TipoValidador>
    {
        public TipoValidadorConfiguration()
        {
            HasKey(c => c.TipoValidadorID);
            Property(c => c.TipoValidadorID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(c => c.Descricao).HasMaxLength(50);

            HasRequired(c => c.TipoPergunta)
                .WithMany()
                .HasForeignKey(c => c.TipoPerguntaID);
        }
    }
}
