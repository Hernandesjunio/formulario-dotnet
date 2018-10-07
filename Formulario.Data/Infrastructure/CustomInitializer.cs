using Formulario.Business.Class;
using Formulario.Business;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Infrastructure
{
    public class CustomInitializer : DropCreateDatabaseAlways<FormularioContext>
    {
        protected override void Seed(FormularioContext context)
        {
            base.Seed(context);

            var typeTipoEntrada = typeof(eTipoEntrada);
            var typeTipoPergunta = typeof(eTipoPergunta);
            var typeValidador = typeof(eTipoValidador);

            var lstTipoPergunta = Enum.GetValues(typeTipoPergunta).Cast<eTipoPergunta>()
                .Select(c => new TipoPergunta
                {
                    Descricao = c.GetEnumDescription(),
                    TipoPerguntaID = (byte)c
                })
                .ToList();

            var lstTipoEntrada = Enum.GetValues(typeTipoEntrada).Cast<eTipoEntrada>()
                .Select(c => new TipoEntrada
                {
                    Descricao = c.GetEnumDescription(),
                    TipoEntradaID = (byte)c,
                    TipoPerguntaID = (byte)c.GetType().GetField(c.ToString()).CustomAttributes.Where(e => e.AttributeType == typeof(TipoPerguntaAttribute)).FirstOrDefault().ConstructorArguments.First().Value
                })
                .ToList();

            var lstValidador = Enum.GetValues(typeValidador).Cast<eTipoValidador>()
                .Select(c => new TipoValidador
                {
                    Descricao = c.GetEnumDescription(),
                    TipoValidadorID = (byte)c,
                    TipoPerguntaID = (byte)c.GetType().GetField(c.ToString()).CustomAttributes.Where(e => e.AttributeType == typeof(TipoPerguntaAttribute)).FirstOrDefault().ConstructorArguments.First().Value
                })
                .ToList();


            context.TipoPergunta.AddRange(lstTipoPergunta);
            int qtd = context.SaveChanges();
            context.TipoEntrada.AddRange(lstTipoEntrada);
            qtd = context.SaveChanges();
            context.TipoValidador.AddRange(lstValidador);
            qtd = context.SaveChanges();

            context.Database.ExecuteSqlCommand("ALTER TABLE dbo.Pergunta ADD CONSTRAINT [FK_dbo.Pergunta_dbo.TipoPergunta] FOREIGN KEY (TipoPerguntaID)REFERENCES dbo.TipoPergunta(TipoPerguntaID)");
            context.Database.ExecuteSqlCommand("ALTER TABLE dbo.PerguntaCondicional ADD CONSTRAINT [FK_dbo.PerguntaCondicional_dbo.TipoPergunta] FOREIGN KEY (TipoPerguntaID)REFERENCES dbo.TipoPergunta(TipoPerguntaID)");
            context.Database.ExecuteSqlCommand("ALTER TABLE dbo.Pergunta ADD CONSTRAINT [FK_dbo.Pergunta_dbo.TipoValidador] FOREIGN KEY (TipoValidadorID)REFERENCES dbo.TipoValidador(TipoValidadorID)");
            context.Database.ExecuteSqlCommand("ALTER TABLE dbo.Resposta ADD CONSTRAINT [FK_dbo.Resposta_dbo.TipoPergunta] FOREIGN KEY (TipoRespostaID)REFERENCES dbo.TipoPergunta(TipoPerguntaID)");

            //"ADD CONSTRAINT CK_SomeTable_SomeColumn CHECK (SomeColumn >= X);")"

        }
    }
}
