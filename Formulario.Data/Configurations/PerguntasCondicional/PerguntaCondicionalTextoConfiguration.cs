using Formulario.Business.Perguntas.Concicional;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.PerguntasCondicional
{
    public class PerguntaCondicionalTextoConfiguration : EntityTypeConfiguration<PerguntaCondicionalTexto>
    {
        public PerguntaCondicionalTextoConfiguration()
        {
            Property(c => c.ValorAtivacao).IsRequired().HasColumnName("ValorAtivacaoTexto");
            Property(c => c.Operacao).IsRequired().HasColumnName("OperacaoID");
        }
    }
}
