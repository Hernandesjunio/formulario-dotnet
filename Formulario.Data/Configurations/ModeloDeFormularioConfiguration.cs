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
    public class ModeloDeFormularioConfiguration : EntityTypeConfiguration<ModeloDeFormulario>
    {
        public ModeloDeFormularioConfiguration()
        {
            HasKey(c => c.ModeloFormularioID);
            Property(c => c.ModeloFormularioID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("ModeloDeFormulario", "dbo");

            Property(c => c.Descricao).HasMaxLength(50);
            Property(c => c.Html).HasColumnType("text");

            HasMany(c => c.Perguntas)
                .WithRequired(c => c.ModeloDeFormulario)
                .HasForeignKey(c => c.ModeloDeFormularioID)
                .WillCascadeOnDelete(false);

            Property(c => c.ControleAtualizacao.UsuarioID).HasColumnName("UsuarioAtualizacao");
            Property(c => c.ControleAtualizacao.Data).HasColumnName("DataAtualizacao");
        }
    }
}
