using Formulario.Business.Perguntas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Perguntas
{
    public class PerguntaTextoConfiguration : EntityTypeConfiguration<PerguntaTexto>
    {
        public PerguntaTextoConfiguration()
        {
            //Property(c => c.Validador).HasColumnName("ValidadorID");
            Property(c => c.TipoEntradaID).HasColumnName("TipoEntradaID");

            Property(c => c.TamanhoMaximo).IsRequired();
            Property(c => c.PatternRegex).IsOptional();
        }
    }
}
