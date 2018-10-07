using Formulario.Business.Perguntas.Concicional;
using Formulario.Business.Perguntas.Misc;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations
{
    public class OpcaoAtivacaoConfiguration : EntityTypeConfiguration<OpcaoAtivacao>
    {
        public OpcaoAtivacaoConfiguration()
        {
            HasKey(c => new { c.OpcaoID, c.PerguntaCondicionalID });

            HasRequired(c => c.Opcao)
                .WithMany()
                .HasForeignKey(c => c.OpcaoID);

            HasRequired(c => c.PerguntaCondicional)
                .WithMany(c => c.OpcoesAtivacao)
                .HasForeignKey(c => c.PerguntaCondicionalID);

            Property(c => c.ControleAtualizacao.UsuarioID).HasColumnName("UsuarioAtualizacao");
            Property(c => c.ControleAtualizacao.Data).HasColumnName("DataAtualizacao");
        }
    }
}
