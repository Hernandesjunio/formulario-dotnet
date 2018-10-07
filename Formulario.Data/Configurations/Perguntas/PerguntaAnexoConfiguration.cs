using Formulario.Business.Perguntas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Perguntas
{
    public class PerguntaAnexoConfiguration : EntityTypeConfiguration<PerguntaAnexo>
    {
        public PerguntaAnexoConfiguration()
        {
            Property(c => c.TamanhoMaximoBytes).IsRequired();
            Property(c => c.TipoEntradaID).HasColumnName("TipoEntradaID");
        }
    }
}
