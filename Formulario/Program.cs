using Formulario.ComplexProperties;
using Formulario.Data.Configurations;
using Formulario.Data.Infrastructure;
using Formulario.DTO;
using Formulario.Enumeradores;
using Formulario.Formulario;
using Formulario.Leiaute;
using Formulario.Perguntas;
using Formulario.Perguntas.Concicional;
using Formulario.Perguntas.Misc;
using Formulario.RepositoryPattern;
using Formulario.Respostas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Formulario
{

    


    public class DatabaseFactoryMock : IDatabaseFactory
    {
        protected IDatabaseContext ctx = null;

        public DatabaseFactoryMock()
        {

        }

        public IDatabaseContext Get()
        {
            return ctx = ctx ?? new DatabaseContextMock();
        }
    }

    public class RepositoryMock<T> : IRepository<T> where T : class
    {
        static HashSet<T> database = new HashSet<T>();

        public T1 Create<T1>()
        {
            throw new NotImplementedException();
        }

        public void Delete(T obj)
        {
            if (database.Contains(obj))
                database.Remove(obj);
        }

        public void Detach(T obj)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }

        public IQueryable<T> GetQuery(bool TrackingChanges = true)
        {
            return database.AsQueryable();
        }

        public T Insert(T obj)
        {
            database.Add(obj);
            return obj;
        }

        public void Update(T obj)
        {
            database.Remove(obj);
            database.Add(obj);
        }
    }

    public class DatabaseContextMock : IDatabaseContext
    {
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new RepositoryMock<T>();
        }
    }

    public class UnitOfWorkMock<T> : IUnitOfWOrkGeneric<T> where T : IDatabaseFactory
    {
        private readonly T factory;

        public UnitOfWorkMock(T factory)
        {
            this.factory = factory;
        }

        public int Commit(string UserID)
        {
            return 1;
        }

        public async Task<int> CommitAsync(string UserID)
        {
            return await Task.Run(() => { return 1; });
        }

        public void Rollback()
        {

        }
    }

    class Program
    {
        static long Identity = 0;

        static Expression<Func<PerguntaTexto, dynamic>> selector = user => new
        {
            user.TamanhoMaximo,
            user.Descricao,
            Qtd = user.LeiautePerguntas.Count()
        };

        static void Main(string[] args)
        {
            IDatabaseFactory factory = new DatabaseFactoryFormulario();
            var ctx = factory.Get();

            //TestarModeloDeDominio();
            PerguntaService svc = new PerguntaService(factory, new UnitOfWorkGeneric(factory));
            
            svc.BuscarModeloDeFormulario().ToList();

            ModeloFormularioDTO formDTO = new ModeloFormularioDTO();
            formDTO.Descricao = "Modelo1";
            formDTO.Html = "<p>Teste</p>";

            formDTO.ModeloFormularioID = 1;

            var sexoOpts = new List<OpcaoDTO>();
            sexoOpts.Add(-1, "Masculino");
            sexoOpts.Add(-2, "Feminino");

            var dispensadoOpts = new List<OpcaoDTO>();
            dispensadoOpts.Add(-2, "Sim");
            dispensadoOpts.Add(-3, "Não");

            var avaliacaoOpts = new List<OpcaoDTO>();
            avaliacaoOpts.Add(-4, "Bom");
            avaliacaoOpts.Add(-5, "Ruim");
            avaliacaoOpts.Add(-6, "Péssimo");

            var linhasGrade = new List<LinhasGradeDTO>();
            linhasGrade.Add(-1, "Conhecimento 1");
            linhasGrade.Add(-2, "Conhecimento 2");
            linhasGrade.Add(-3, "Conhecimento 3");
            linhasGrade.Add(-4, "Conhecimento 4");

            formDTO.Perguntas = new List<PerguntaDTO>
            {
                new PerguntaDTO
                {
                    Deleted=false,
                    Titulo = "Nome",
                    Descricao=null,
                    //GradeOpcoes=null,
                    Obrigatorio=true,
                    //Opcoes=sexoOpts,
                    PerguntaCondicional=null,
                    PerguntaCondicionalID=null,
                    PerguntaID = -1,
                    //PerguntasGrade=null,
                    TipoPergunta = eTipoPergunta.Texto,
                    TipoEntrada = (byte) eTipoEntrada.Texto_CaixaDeTexto,
                    PatternRegex = null,
                    TamanhoMaximo = 50,
                    //Validador =(short) eValidador.Texto_NaoValidar,
                },
                new PerguntaDTO
                {
                    Deleted=false,
                    Titulo = "Sexo",
                    Descricao=null,
                    //GradeOpcoes=null,
                    Obrigatorio=true,
                    Opcoes = sexoOpts,
                    PerguntaCondicional=null,
                    PerguntaCondicionalID=null,
                    PerguntaID = -2,
                    //PerguntasGrade=null,
                    TipoPergunta = eTipoPergunta.EscolhaUnica,
                    TipoEntrada = (byte) eTipoEntrada.EscolhaUnica_Dropdown,
                    PatternRegex = null,
                    TamanhoMaximo = 50,
                    //Validador = (short) eTipoValidador.Texto_NaoValidar,
                },
                new PerguntaDTO
                {
                    Deleted=false,
                    Titulo = "Dispensado Reservista",
                    Descricao=null,
                    //GradeOpcoes=null,
                    Obrigatorio=false,
                    PerguntaCondicional= new PerguntaCondicionalDTO {
                                        TipoPergunta = eTipoPergunta.EscolhaUnica,
                                        OpcoesAtivacao = null,
                                        PerguntaCondicionalID=0,
                                        PerguntaID = -2,
                                        PerguntasGrade=null,
                                        ValorAtivacao = -1,
                                        OperacaoCondicional= formDTO.LstOperacaoCondicional[eTipoPergunta.EscolhaUnica].Single(d=>d.Key == (short) eOperacaoCondicional.UnicaOpcao_Igual).Key,
                    },
                     Opcoes=dispensadoOpts,
                    PerguntaCondicionalID=0,
                    PerguntaID = -3,
                    //PerguntasGrade=null,
                    TipoPergunta = eTipoPergunta.EscolhaUnica,
                    TipoEntrada = (byte) eTipoEntrada.EscolhaUnica_Dropdown,
                    PatternRegex = null,
                    TamanhoMaximo = null,
                    //Validador = (short) eValidador.Texto_NaoValidar,
                },
                new PerguntaDTO
                {
                    Deleted=false,
                    Titulo = "Avaliação",
                    Descricao="<p>Avaliação</p>",
                    //GradeOpcoes=null,
                    Obrigatorio=false,
                    Opcoes = avaliacaoOpts,
                    LinhasGrade = linhasGrade,
                    CasasDecimais=null,
                    PerguntaCondicional=null,
                    PerguntaCondicionalID=null,
                    PerguntaID = -4,
                    //PerguntasGrade=null,
                    TipoPergunta = eTipoPergunta.Grade,
                    TipoEntrada = (byte) eTipoEntrada.Grade_Radio,
                    PatternRegex = null,
                    TamanhoMaximo = null,
                    //Validador = (short) eTipoValidador.Texto_NaoValidar,
                },
                new PerguntaDTO
                {
                    Deleted=false,
                    Titulo = "Avaliação",
                    Descricao="<p>Avaliação</p>",
                    //GradeOpcoes=null,
                    Obrigatorio=false,
                    Opcoes = avaliacaoOpts,
                    LinhasGrade = linhasGrade,
                    CasasDecimais=null,
                    PerguntaCondicional=null,
                    PerguntaCondicionalID=null,
                    PerguntaID = -4,
                    //PerguntasGrade=null,
                    TipoPergunta = eTipoPergunta.Numero,
                    TipoEntrada = (byte) eTipoEntrada.Numero_CaixaDeTexto,
                    PatternRegex = null,
                    TamanhoMaximo = null,
                    //Validador = (short) eValidador.Texto_NaoValidar,
                },
            };

            using (var transaction = new TransactionScope())
            {
                try
                {
                    var modelo = svc.GravarModeloFormulario(formDTO, "ADMIN");
                    svc.Commit("ADMIN");

                    var query = factory.Get().GetRepository<PerguntaTexto>().GetQuery();

                    var teste = query.Select(selector).ToList();

                    var mod = svc.AbrirFormulario(modelo.ModeloFormularioID);

                    var modResp = svc.ResponderFormulario(mod.ModeloFormularioID);

                    var resp1 = modResp.Respostas[0];
                    resp1.Valor = "Hernandes";
                    var resp2 = modResp.Respostas[1];
                    resp2.OpcaoID = modResp.ModeloFormulario.Perguntas[1].Opcoes.First().OpcaoID;

                    var resp = svc.GravarRespostaModeloFormulario(modResp, "ADMIN");
                    var qtd = svc.Commit("ADMIN");

                    var testeAbrir = svc.AbrirRespostaModeloFormulario(resp.RespostaModeloFormularioID);

                }
                catch (Exception)
                {
                    throw;
                }
            }


        }

        private static void TestarModeloDeDominio()
        {
            var multipla = new PerguntaMultiplaEscolha
            {
                Obrigatorio = false,
                PerguntaCondicional = null,
                //TipoPergunta = Enumeradores.eMultiplaEscolha.Checkbox,
                TipoEntradaID = (byte)eTipoEntrada.MultiplaEscolha_CaixaDeSelecao,
                PerguntaID = ++Identity,
                Titulo = "Linguagem",
                Descricao = null,
                Opcoes = new List<Opcao>
                            {
                                new Opcao
                                {
                                     OpcaoID=1,
                                     Descricao="C#"
                                },
                                new Opcao
                                {
                                     OpcaoID=2,
                                     Descricao="JAVA"
                                },
                                new Opcao
                                {
                                    OpcaoID =3,
                                    Descricao="JavaScript"
                                },
                                new Opcao
                                {
                                     OpcaoID = 4,
                                    Descricao="Python"
                                },
                            }
            };

            var texto = new PerguntaTexto
            {
                PerguntaID = ++Identity,
                Titulo = "Nome",
                TipoEntradaID = (byte)eTipoEntrada.Texto_CaixaDeTexto,
                Descricao = null,
                PatternRegex = null,
                PerguntaCondicional = null,
                TamanhoMaximo = 50,
                TipoValidadorID = null
            };

            var unica = new PerguntaEscolhaUnica
            {
                PerguntaID = ++Identity,
                Descricao = null,
                Titulo = "Sexo",
                PerguntaCondicional = null,
                Opcoes = new List<Opcao>
                           {
                                new Opcao
                                {
                                     Descricao="M",
                                },
                                new Opcao
                                {
                                     Descricao="F",
                                }
                           },
                //TipoPergunta = Enumeradores.eEscolhaUnica.Radio,
                TipoEntradaID = (byte)eTipoEntrada.EscolhaUnica_Radio
            };

            var unicaCondicional = new PerguntaEscolhaUnica
            {
                PerguntaID = ++Identity,
                Titulo = "Nível de Conhecimento C#",
                Descricao = null,
                //TipoPergunta = Enumeradores.eEscolhaUnica.Star,
                TipoEntradaID = (byte)eTipoEntrada.EscolhaUnica_Star,
                PerguntaCondicional = new PerguntaCondicionalMultipla
                {
                    PerguntaID = 1,
                    OpcoesAtivacao = multipla.Opcoes.Where(c => c.OpcaoID == 1)
                    .Select(c => new OpcaoAtivacao
                    {
                        OpcaoID = c.OpcaoID,
                        Opcao = c,
                        //PerguntaCondicionalID = pCondicional.PerguntaCondicionalID,
                        //PerguntaCondicional = this
                    })
                    .ToList(),
                    Operacao = Enumeradores.eOperacaoCondicional.MultiplaOpcoes_Contem,
                    Pergunta = multipla,
                },
                Opcoes = new List<Opcao>
                            {
                                 new Opcao{ Descricao="Básico"},
                                 new Opcao{ Descricao="Médio"},
                                 new Opcao{ Descricao="Avançado"},
                            }
            };

            var pIdade = new PerguntaNumero
            {
                PerguntaID = ++Identity,
                CasasDecimais = 0,
                Descricao = null,
                Titulo = "Idade",
                Obrigatorio = true,
                PerguntaCondicional = null,
                Prefixo = null,
                Sufixo = null,
                TipoEntradaID = (byte)eTipoEntrada.Numero_CaixaDeTexto,
                TipoValidadorID = eTipoValidador.Numero_MaiorIgualZero
            };

            var pCondicionalIdade = new PerguntaTexto
            {
                PerguntaID = ++Identity,
                Descricao = null,
                Titulo = "Teste Condicional",
                Obrigatorio = true,
                PatternRegex = null,
                TamanhoMaximo = 50,
                TipoEntradaID = (byte)eTipoEntrada.Texto_CaixaDeTexto,
                TipoValidadorID = null,
                PerguntaCondicional = new PerguntaCondicionalNumero
                {
                    Operacao = eOperacaoCondicional.Numero_Igual,
                    Pergunta = pIdade,
                    PerguntaID = 2,
                    ValorAtivacao = 20
                }
            };

            var opcoesGrade = new HashSet<Opcao>
                               {
                                   new Opcao { Descricao="Básico", OpcaoID=1 },
                                   new Opcao { Descricao="Intermediário", OpcaoID=2 },
                                   new Opcao { Descricao="Avançado", OpcaoID=3 }
                               };

            var grade = new PerguntaGradeDeOpcoes
            {
                PerguntaID = ++Identity,
                Descricao = null,
                Obrigatorio = false,
                Titulo = "Avaliacao",
                PerguntaCondicional = null,
                Opcoes = opcoesGrade,
                Linhas = new HashSet<LinhaPerguntaGrade>
                     {
                          new LinhaPerguntaGrade
                          {
                              //PerguntaID = ++Identity,
                              //Descricao = null,
                              Titulo = "C#",
                              //Obrigatorio = false,
                               //Opcoes=opcoesGrade
                          },
                            new LinhaPerguntaGrade
                        {
                            //Descricao = null,
                            Titulo = "Java",
                            //Obrigatorio = false,
                            //Opcoes=opcoesGrade
                        }
                     }
            };

            ModeloDeFormulario modelo = new ModeloDeFormulario
            {
                Perguntas = new List<Pergunta>
                 {
                     texto,
                     unica,
                     multipla,
                     unicaCondicional,
                     pIdade,
                     pCondicionalIdade
                 }
            };

            modelo.CriarLeiautePerguntasPadrao();

            RespostaModeloDeFormulario resposta = new RespostaModeloDeFormulario
            {
                ModeloDeFormulario = modelo,
                RespostaModeloFormularioID = 1,
                Respostas = new HashSet<Resposta>
                 {
                     new RespostaTexto
                     {
                          Pergunta = texto,
                          RespostaID=1,
                          Valor ="Hernandes"
                     },
                     new RespostaNumero
                     {
                          Pergunta=pIdade,
                           RespostaID=6,
                            Valor=20,
                     },
                     new RespostaUnica
                     {
                          RespostaID=2,
                           Pergunta=unica,
                            OpcaoEscolhida = unica.Opcoes.First()
                     },
                     new RespostaMultipla
                     {
                         RespostaID=3,
                          Pergunta=multipla,
                           OpcoesEscolhida = multipla.Opcoes.Skip(1).Take(2).Select(c=> new OpcaoRespondida { OpcaoID=c.OpcaoID }).ToList(),
                     },
                     //new RespostaUnica
                     //{
                     //     RespostaID=3,
                     //      Pergunta = unicaCondicional,
                     //       OpcaoEscolhida = unicaCondicional.Opcoes.First(),
                     //        OpcaoEscolhidaID=unicaCondicional.Opcoes.First().OpcaoID,
                     //},
                     new RespostaTexto
                     {
                          Pergunta=pCondicionalIdade,
                           Valor="A",
                     }
                 }
            };

            resposta.Validar();
        }
    }
}
