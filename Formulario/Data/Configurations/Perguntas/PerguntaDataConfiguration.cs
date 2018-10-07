using Formulario.Perguntas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Perguntas
{
    public class PerguntaDataConfiguration : EntityTypeConfiguration<PerguntaData>
    {
        public PerguntaDataConfiguration()
        {
            //Property(c => c.TipoValidadorID).HasColumnName("ValidadorID");
            Property(c => c.TipoEntradaID).HasColumnName("TipoEntradaID");
        }
    }
}
