using Formulario.ComplexProperties;
using Formulario.Business.Perguntas.Concicional;
using Formulario.Business.Respostas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations
{    
    public class OpcaoRespondidaConfiguration : EntityTypeConfiguration<OpcaoRespondida>
    {
        public OpcaoRespondidaConfiguration()
        {
            HasKey(c => new { c.OpcaoID, c.RespostaID });

            HasRequired(c => c.Opcao)
                .WithMany()
                .HasForeignKey(c => c.OpcaoID);

            HasRequired(c => c.Resposta)
                .WithMany(c => c.OpcoesEscolhida)
                .HasForeignKey(c => c.RespostaID);

            Property(c => c.ControleAtualizacao.UsuarioID).HasColumnName("UsuarioAtualizacao");
            Property(c => c.ControleAtualizacao.Data).HasColumnName("DataAtualizacao");
        }
    }
}
