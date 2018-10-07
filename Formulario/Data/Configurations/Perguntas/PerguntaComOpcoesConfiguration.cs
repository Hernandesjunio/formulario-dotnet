using Formulario.Perguntas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Perguntas
{
    public class PerguntaComOpcoesConfiguration : EntityTypeConfiguration<PerguntaComOpcoes>
    {
        public PerguntaComOpcoesConfiguration()
        {
            HasMany(c => c.Opcoes)
                .WithRequired(c => c.PerguntaComOpcoes)
                .HasForeignKey(c => c.PerguntaID)
                .WillCascadeOnDelete(false);
        }
    }
}
