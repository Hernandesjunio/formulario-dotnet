using Formulario.Class;
using Formulario.Data.Configurations;
using Formulario.Data.Configurations.Perguntas;
using Formulario.Data.Configurations.PerguntasCondicional;
using Formulario.Data.Configurations.Respostas;
using Formulario.Enumeradores;
using Formulario.Perguntas;
using Formulario.Perguntas.Concicional;
using Formulario.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Infrastructure
{
   

    public class FormularioContext : DbContext, IDatabaseContext
    {
        public FormularioContext()
            : base("cnn")
        {
            //Database.SetInitializer<FormularioContext>(null);
            Database.SetInitializer(new CustomInitializer());
        }

        public DbSet<TipoValidador> TipoValidador { get; set; }
        public DbSet<Pergunta> Pergunta { get; set; }
        public DbSet<TipoPergunta> TipoPergunta { get; set; }
        public DbSet<TipoEntrada> TipoEntrada { get; set; }
        public DbSet<PerguntaCondicional> PerguntaCondicional { get; set; }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new RepositoryGeneric<T>(this);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new TipoEntradaConfiguration());
            modelBuilder.Configurations.Add(new TipoOperacaoCondicionalConfiguration());
            modelBuilder.Configurations.Add(new TipoValidadorConfiguration());

            modelBuilder.Configurations.Add(new PerguntaCondicionalConfiguration());
            modelBuilder.Configurations.Add(new PerguntaConfiguration());
            modelBuilder.Configurations.Add(new LinhaPerguntaGradeConfiguration());
            modelBuilder.Configurations.Add(new PerguntaTextoConfiguration());
            modelBuilder.Configurations.Add(new PerguntaNumeroConfiguration());
            modelBuilder.Configurations.Add(new PerguntaMultiplaEscolhaConfiguration());
            modelBuilder.Configurations.Add(new PerguntaGradeDeOpcoesConfiguration());
            modelBuilder.Configurations.Add(new PerguntaEscolhaUnicaConfiguration());
            modelBuilder.Configurations.Add(new PerguntaDataConfiguration());
            modelBuilder.Configurations.Add(new PerguntaAnexoConfiguration());

            modelBuilder.Configurations.Add(new PerguntaCondicionalUnicaConfiguration());
            modelBuilder.Configurations.Add(new PerguntaCondicionalTextoConfiguration());
            modelBuilder.Configurations.Add(new PerguntaCondicionalNumeroConfiguration());
            modelBuilder.Configurations.Add(new PerguntaCondicionalMultiplaConfiguration());
            modelBuilder.Configurations.Add(new PerguntaCondicionalGradeConfiguration());
            modelBuilder.Configurations.Add(new PerguntaCondicionalDataConfiguration());
            modelBuilder.Configurations.Add(new PerguntaCondicionalAnexoConfiguration());
            modelBuilder.Configurations.Add(new OpcaoAtivacaoConfiguration());
            modelBuilder.Configurations.Add(new PerguntaCondicionalOpcoesMultiplaConfiguration());
            modelBuilder.Configurations.Add(new PerguntaComOpcoesConfiguration());

            modelBuilder.Configurations.Add(new OpcaoRespondidaConfiguration());
            modelBuilder.Configurations.Add(new RespostaAnexoConfiguration());
            modelBuilder.Configurations.Add(new RespostaConfiguration());
            modelBuilder.Configurations.Add(new RespostaDataConfiguration());
            modelBuilder.Configurations.Add(new RespostaGradeConfiguration());
            modelBuilder.Configurations.Add(new RespostaLinhaPerguntaGradeConfiguration());
            modelBuilder.Configurations.Add(new RespostaMultiplaConfiguration());
            modelBuilder.Configurations.Add(new RespostaNumeroConfiguration());
            modelBuilder.Configurations.Add(new RespostaTextoConfiguration());
            modelBuilder.Configurations.Add(new RespostaUnicaConfiguration());

            modelBuilder.Configurations.Add(new RespostaModeloDeFormularioConfiguration());
            modelBuilder.Configurations.Add(new ModeloDeFormularioConfiguration());
            modelBuilder.Configurations.Add(new ControleConfiguration());
            modelBuilder.Configurations.Add(new TipoPerguntaConfiguration());

            modelBuilder.Configurations.Add(new LeiautePerguntaConfiguration());
            modelBuilder.Configurations.Add(new LeiautePerguntaItemConfiguration());
            modelBuilder.Configurations.Add(new RespostaAnexoContentConfiguration());
            modelBuilder.Configurations.Add(new OpcaoConfiguration());

            modelBuilder.Configurations.Add(new AnexoConfiguration());
            //modelBuilder.Configurations.Add(new RespostaModeloDeFormularioConfiguration());
        }
    }
}
