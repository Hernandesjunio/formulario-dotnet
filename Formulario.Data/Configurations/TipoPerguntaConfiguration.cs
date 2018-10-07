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
    public class TipoPerguntaConfiguration : EntityTypeConfiguration<TipoPergunta>
    {
        public TipoPerguntaConfiguration()
        {
            ToTable("TipoPergunta");
            HasKey(c => c.TipoPerguntaID);
            Property(c => c.TipoPerguntaID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(c => c.Descricao).HasMaxLength(50);
                        
        }
    }
}
