using Formulario.Leiaute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations
{
    public class LeiautePerguntaItemConfiguration : EntityTypeConfiguration<LeiautePerguntaItem>
    {
        public LeiautePerguntaItemConfiguration()
        {
            ToTable("LeiautePerguntaItem", "dbo");
            HasKey(c => c.LeiautePerguntaItemID);
            Property(c => c.LeiautePerguntaItemID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.Responsivo).IsRequired();
            Property(c => c.Tamanho).IsRequired();

            Property(c => c.ControleAtualizacao.UsuarioID).HasColumnName("UsuarioAtualizacao");
            Property(c => c.ControleAtualizacao.Data).HasColumnName("DataAtualizacao");                        
        }
    }
}
