using Formulario.Class;
using Formulario.Respostas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations
{
    public class RespostaAnexoContentConfiguration : EntityTypeConfiguration<RespostaAnexoContent>
    {
        public RespostaAnexoContentConfiguration()
        {            
            HasRequired(c => c.Resposta)
                .WithRequiredDependent()
                .Map(c => c.MapKey("RespostaID"));

        }
    }

    public class AnexoConfiguration : EntityTypeConfiguration<Anexo>
    {
        public AnexoConfiguration()
        {
            HasKey(c => c.AnexoID);
            Property(c => c.AnexoID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Nome).IsRequired().HasMaxLength(200);
            Property(c => c.Extensao).IsRequired().HasMaxLength(50);
            Property(c => c.UniqueIdentifier).IsRequired().HasColumnType("uniqueidentifier");
            Property(c => c.Conteudo).IsRequired().IsMaxLength();
            Property(c => c.ControleUsuario.UsuarioID).HasColumnName("UsuarioID");
            Property(c => c.ControleUsuario.Data).HasColumnName("DataAtualizacao");

            //TPT
            Map(c => c.ToTable("Anexo", "dbo"))
                .Map<RespostaAnexoContent>(c => c.ToTable("RespostaAnexoContent", "dbo"));
        }
    }
}
