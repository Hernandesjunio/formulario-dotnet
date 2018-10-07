using Formulario.Perguntas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Perguntas
{
    public class PerguntaNumeroConfiguration : EntityTypeConfiguration<PerguntaNumero>
    {
        public PerguntaNumeroConfiguration()
        {
            //Property(c => c.Validador).HasColumnName("ValidadorID");
            Property(c => c.TipoEntradaID).HasColumnName("TipoEntradaID");

            Property(c => c.Sufixo).IsOptional();
            Property(c => c.Prefixo).IsOptional();
            Property(c => c.CasasDecimais).IsRequired();
        }
    }
}
