using Formulario.Enumeradores;
using Formulario.Leiaute;
using Formulario.Perguntas;
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
    public class RespostaConfiguration : EntityTypeConfiguration<Resposta>
    {
        public RespostaConfiguration()
        {
            HasKey(c => c.RespostaID);
            Property(c => c.RespostaID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(c => c.Pergunta)
                .WithMany()
                .HasForeignKey(c => c.PerguntaID)
                .WillCascadeOnDelete(false);

            ToTable("Resposta", "dbo")
                .Map<RespostaAnexo>(c => c.Requires("TipoRespostaID").HasValue((byte)eTipoPergunta.Anexo))
                .Map<RespostaData>(c => c.Requires("TipoRespostaID").HasValue((byte)eTipoPergunta.Data))
                .Map<RespostaGrade>(c => c.Requires("TipoRespostaID").HasValue((byte)eTipoPergunta.Grade))                
                .Map<RespostaMultipla>(c => c.Requires("TipoRespostaID").HasValue((byte)eTipoPergunta.MultiplaEscolha))
                .Map<RespostaNumero>(c => c.Requires("TipoRespostaID").HasValue((byte)eTipoPergunta.Numero))
                .Map<RespostaTexto>(c => c.Requires("TipoRespostaID").HasValue((byte)eTipoPergunta.Texto))
                .Map<RespostaUnica>(c => c.Requires("TipoRespostaID").HasValue((byte)eTipoPergunta.EscolhaUnica));

            Property(c => c.ControleAtualizacao.UsuarioID).HasColumnName("UsuarioAtualizacao");
            Property(c => c.ControleAtualizacao.Data).HasColumnName("DataAtualizacao");
        }
    }    
}
