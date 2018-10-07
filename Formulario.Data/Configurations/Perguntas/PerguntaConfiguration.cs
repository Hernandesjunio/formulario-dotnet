using Formulario.Business;
using Formulario.Business.Perguntas;
using Formulario.Business.Perguntas.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Perguntas
{
    

    public class PerguntaConfiguration : EntityTypeConfiguration<Pergunta>
    {
        public PerguntaConfiguration()
        {
            HasKey(c => c.PerguntaID);
            Property(c => c.PerguntaID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("Pergunta", "dbo")
                .Map<PerguntaAnexo>(c => c.Requires("TipoPerguntaID").HasValue((byte)eTipoPergunta.Anexo))
                .Map<PerguntaData>(c => c.Requires("TipoPerguntaID").HasValue((byte)eTipoPergunta.Data))
                .Map<PerguntaEscolhaUnica>(c => c.Requires("TipoPerguntaID").HasValue((byte)eTipoPergunta.EscolhaUnica))
                .Map<PerguntaGradeDeOpcoes>(c => c.Requires("TipoPerguntaID").HasValue((byte)eTipoPergunta.Grade))
                .Map<PerguntaMultiplaEscolha>(c => c.Requires("TipoPerguntaID").HasValue((byte)eTipoPergunta.MultiplaEscolha))
                .Map<PerguntaNumero>(c => c.Requires("TipoPerguntaID").HasValue((byte)eTipoPergunta.Numero))
                .Map<PerguntaTexto>(c => c.Requires("TipoPerguntaID").HasValue((byte)eTipoPergunta.Texto));

            Property(c => c.Titulo).IsRequired().HasMaxLength(50);
            Property(c => c.Descricao).IsOptional().IsMaxLength();

            HasRequired(c => c.TipoEntrada)
                .WithMany()
                .HasForeignKey(c => c.TipoEntradaID);

            HasOptional(c => c.PerguntaCondicional)
                .WithMany()
                .HasForeignKey(c => c.PerguntaCondicionalID);

            Property(c => c.ControleAtualizacao.UsuarioID).HasColumnName("UsuarioAtualizacao");
            Property(c => c.ControleAtualizacao.Data).HasColumnName("DataAtualizacao");
        }
    }
}
