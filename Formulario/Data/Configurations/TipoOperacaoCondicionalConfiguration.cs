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
    public class TipoOperacaoCondicionalConfiguration : EntityTypeConfiguration<TipoOperacaoCondicional>
    {
        public TipoOperacaoCondicionalConfiguration()
        {
            HasKey(c => c.TipoOperacaoCondicionalID);
            Property(c => c.TipoOperacaoCondicionalID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(c => c.Descricao).HasMaxLength(50);

            HasRequired(c => c.TipoPergunta)
                .WithMany()
                .HasForeignKey(c => c.TipoPerguntaID);
        }
    }
}
