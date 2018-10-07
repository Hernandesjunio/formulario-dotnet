using Formulario.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations
{
    public class RespostaModeloDeFormularioConfiguration : EntityTypeConfiguration<RespostaModeloDeFormulario>
    {
        public RespostaModeloDeFormularioConfiguration()
        {
            HasKey(c => c.RespostaModeloFormularioID);
            Property(c => c.RespostaModeloFormularioID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("RespostaModeloDeFormulario", "dbo");

            HasRequired(c => c.ModeloDeFormulario)
                .WithMany()
                .HasForeignKey(c => c.ModeloDeFormularioID);

            HasMany(c => c.Respostas)
                .WithRequired(c => c.RespostaModeloDeFormulario)
                .HasForeignKey(c => c.RespostaModeloFormularioID)
                .WillCascadeOnDelete(false);

            Property(c => c.ControleAtualizacao.UsuarioID).HasColumnName("UsuarioAtualizacao");
            Property(c => c.ControleAtualizacao.Data).HasColumnName("DataAtualizacao");
        }
    }
}
