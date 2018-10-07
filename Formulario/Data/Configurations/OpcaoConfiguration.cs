using Formulario.Leiaute;
using Formulario.Perguntas;
using Formulario.Perguntas.Concicional;
using Formulario.Perguntas.Misc;
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
    public class OpcaoConfiguration : EntityTypeConfiguration<Opcao>
    {
        public OpcaoConfiguration()
        {
            HasKey(c => c.OpcaoID);
            Property(c => c.OpcaoID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Descricao).IsRequired().HasMaxLength(50);

            HasRequired(c => c.PerguntaComOpcoes)
                .WithMany(c => c.Opcoes)
                .HasForeignKey(c => c.PerguntaID)
                .WillCascadeOnDelete(false);

            Property(c => c.ControleAtualizacao.UsuarioID).HasColumnName("UsuarioAtualizacao");
            Property(c => c.ControleAtualizacao.Data).HasColumnName("DataAtualizacao");
        }
    }
}
