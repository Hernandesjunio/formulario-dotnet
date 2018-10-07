using Formulario.Business;
using Formulario.Business.Perguntas;
using Formulario.Business.Perguntas.Concicional;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations
{
    public class PerguntaCondicionalConfiguration : EntityTypeConfiguration<PerguntaCondicional>
    {
        public PerguntaCondicionalConfiguration()
        {
            HasKey(c => c.PerguntaCondicionalID);

            Property(c => c.PerguntaCondicionalID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("PerguntaCondicional", "dbo")
                .Map<PerguntaCondicionalAnexo>(c => c.Requires("TipoPerguntaID").HasValue((byte)eTipoPergunta.Anexo))
                .Map<PerguntaCondicionalData>(c => c.Requires("TipoPerguntaID").HasValue((byte)eTipoPergunta.Data))
                .Map<PerguntaCondicionalGrade>(c => c.Requires("TipoPerguntaID").HasValue((byte)eTipoPergunta.Grade))
                .Map<PerguntaCondicionalMultipla>(c => c.Requires("TipoPerguntaID").HasValue((byte)eTipoPergunta.MultiplaEscolha))
                .Map<PerguntaCondicionalNumero>(c => c.Requires("TipoPerguntaID").HasValue((byte)eTipoPergunta.Numero))
                .Map<PerguntaCondicionalTexto>(c => c.Requires("TipoPerguntaID").HasValue((byte)eTipoPergunta.Texto))
                .Map<PerguntaCondicionalUnica>(c => c.Requires("TipoPerguntaID").HasValue((byte)eTipoPergunta.EscolhaUnica));

            HasRequired(c => c.Pergunta)
                .WithMany()
                .HasForeignKey(c => c.PerguntaID);

            Property(c => c.ControleAtualizacao.UsuarioID).HasColumnName("UsuarioAtualizacao");
            Property(c => c.ControleAtualizacao.Data).HasColumnName("DataAtualizacao");
        }
    }   
}
