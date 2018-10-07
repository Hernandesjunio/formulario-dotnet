using Formulario.Business.Leiaute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations
{
    public class LeiautePerguntaConfiguration : EntityTypeConfiguration<LeiautePergunta>
    {
        public LeiautePerguntaConfiguration()
        {
            ToTable("LeiautePergunta", "dbo");
            HasKey(c => c.LeiautePerguntaID);
            Property(c => c.LeiautePerguntaID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(c => c.Pergunta)
                .WithMany(c => c.LeiautePerguntas)
                .HasForeignKey(c => c.PerguntaID);

            HasMany(d => d.LeiauteItem)
                .WithRequired(c => c.LeiautePergunta)
                .HasForeignKey(c => c.LeiautePerguntaID);

            Property(c => c.ControleAtualizacao.UsuarioID).HasColumnName("UsuarioAtualizacao");
            Property(c => c.ControleAtualizacao.Data).HasColumnName("DataAtualizacao");
        }
    }
}
