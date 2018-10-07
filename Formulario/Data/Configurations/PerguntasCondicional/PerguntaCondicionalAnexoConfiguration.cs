using Formulario.Perguntas.Concicional;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.PerguntasCondicional
{
    public class PerguntaCondicionalAnexoConfiguration : EntityTypeConfiguration<PerguntaCondicionalAnexo>
    {
        public PerguntaCondicionalAnexoConfiguration()
        {
            Property(c => c.ValorAtivacao).HasColumnName("ValorAtivacaoAnexo");
            Property(c => c.Operacao).HasColumnName("OperacaoID");
        }
    }
}
