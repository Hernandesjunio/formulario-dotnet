using Formulario.Business;
using Formulario.Business.Class;
using Formulario.Business.Leiaute;
using Formulario.Business.Perguntas;
using Formulario.Business.Perguntas.Concicional;
using Formulario.Business.Perguntas.Misc;
using Formulario.Business.RepositoryPattern;
using Formulario.Business.Respostas;
using Formulario.ComplexProperties;
using System.Collections.Generic;
using System.Linq;

namespace Formulario.WebApi.Tests.MockData
{
    public class DatabaseFactoryMock : IDatabaseFactory
    {
        protected IDatabaseContext ctx = null;

        public DatabaseFactoryMock()
        {

        }

        public void Dispose()
        {
            ctx.Dispose();
        }

        public IDatabaseContext Get()
        {
            return ctx = ctx ?? new DatabaseContextMock();
        }

        public static void CreateData(IFormularioService svc,
        IDatabaseFactory factory,
        IUnitOfWOrkGeneric<IDatabaseFactory> unit)
        {
            var ctx = factory.Get();
            //long Identity = 0;

            var repoPergunta = ctx.GetRepository<Pergunta>();
            var repoPerguntaCondicional = ctx.GetRepository<PerguntaCondicional>();
            var repoOpcao = ctx.GetRepository<Opcao>();
            var repoModeloDeFormulario = ctx.GetRepository<ModeloDeFormulario>();
            var repoRespostaModeloDeFormulario = ctx.GetRepository<RespostaModeloDeFormulario>();
            var repoOpcaoAtivacao = ctx.GetRepository<OpcaoAtivacao>();
            var repoLinhaPerguntaGrade = ctx.GetRepository<LinhaPerguntaGrade>();
            var repoResposta = ctx.GetRepository<Resposta>();
            var repoLeiautePergunta = ctx.GetRepository<LeiautePergunta>();
            var repoLeiautePerguntaItem = ctx.GetRepository<LeiautePerguntaItem>();

            var pMultipla = new PerguntaMultiplaEscolha
            {
                Obrigatorio = false,
                PerguntaCondicional = null,
                //TipoPergunta = Enumeradores.eMultiplaEscolha.Checkbox,
                TipoEntradaID = (byte)eTipoEntrada.MultiplaEscolha_CaixaDeSelecao,
                PerguntaID = 1,
                Titulo = "Linguagem",
                Descricao = "Ling",
                ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                Opcoes = new List<Opcao>
                            {
                                new Opcao
                                {
                                     OpcaoID=1,
                                     Descricao="C#",
                                      ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                                },
                                new Opcao
                                {
                                     OpcaoID=2,
                                     Descricao="JAVA",
                                     ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                                },
                                new Opcao
                                {
                                    OpcaoID =3,
                                    Descricao="JavaScript",
                                    ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                                },
                                new Opcao
                                {
                                     OpcaoID = 4,
                                    Descricao="Python",
                                    ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                                },
                            }
            };

            pMultipla.Opcoes.ToList().ForEach(c => repoOpcao.Insert(c));

            repoPergunta.Insert(pMultipla);

            var pTexto = new PerguntaTexto
            {
                PerguntaID = 2,
                Titulo = "Nome",
                TipoEntradaID = (byte)eTipoEntrada.Texto_CaixaDeTexto,
                Descricao = "Desc",
                PatternRegex = null,
                PerguntaCondicional = null,
                TamanhoMaximo = 50,
                TipoValidadorID = null,
                Obrigatorio = true,
                ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
            };

            repoPergunta.Insert(pTexto);

            var pUnica = new PerguntaEscolhaUnica
            {
                PerguntaID = 3,
                Descricao = "Desc Sexo",
                Titulo = "Sexo",
                PerguntaCondicional = null,
                ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                Opcoes = new List<Opcao>
                           {
                                new Opcao
                                {
                                     OpcaoID=5,
                                     Descricao="M",
                                     ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                                },
                                new Opcao
                                {
                                     OpcaoID=6,
                                     Descricao="F",
                                     ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                                }
                           },
                //TipoPergunta = Enumeradores.eEscolhaUnica.Radio,
                TipoEntradaID = (byte)eTipoEntrada.EscolhaUnica_Radio
            };

            pUnica.Opcoes.ToList().ForEach(c => repoOpcao.Insert(c));

            repoPergunta.Insert(pUnica);

            var pUnicaCondicional = new PerguntaEscolhaUnica
            {
                PerguntaID = 4,
                Titulo = "Nível de Conhecimento C#",
                Descricao = "Nível",
                //TipoPergunta = Enumeradores.eEscolhaUnica.Star,
                TipoEntradaID = (byte)eTipoEntrada.EscolhaUnica_Star,
                ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                PerguntaCondicional = new PerguntaCondicionalMultipla
                {

                    PerguntaID = 1,
                    ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                    OpcoesAtivacao = pMultipla.Opcoes.Where(c => c.OpcaoID == 1)
                    .Select(c => new OpcaoAtivacao
                    {
                        OpcaoID = c.OpcaoID,
                        Opcao = c,
                        ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                        //PerguntaCondicionalID = pCondicional.PerguntaCondicionalID,
                        //PerguntaCondicional = this
                    })
                    .ToList(),
                    Operacao = eOperacaoCondicional.MultiplaOpcoes_Contem,
                    Pergunta = pMultipla,
                },
                Opcoes = new List<Opcao>
                            {
                                 new Opcao{ OpcaoID=11, Descricao="Básico",ControleAtualizacao = ControleUsuario.Criar("ADMIN"),},
                                 new Opcao{ OpcaoID=12, Descricao="Médio",ControleAtualizacao = ControleUsuario.Criar("ADMIN"),},
                                 new Opcao{ OpcaoID=13, Descricao="Avançado",ControleAtualizacao = ControleUsuario.Criar("ADMIN"),},
                            }
            };

            (pUnicaCondicional.PerguntaCondicional as PerguntaCondicionalMultipla).OpcoesAtivacao.ToList().ForEach(c => repoOpcaoAtivacao.Insert(c));
            repoPerguntaCondicional.Insert(pUnicaCondicional.PerguntaCondicional);
            pUnicaCondicional.Opcoes.ToList().ForEach(c => repoOpcao.Insert(c));
            repoPergunta.Insert(pUnicaCondicional);

            var pIdade = new PerguntaNumero
            {
                PerguntaID = 5,
                CasasDecimais = 0,
                Descricao = "I",
                Titulo = "Idade",
                Obrigatorio = true,
                PerguntaCondicional = null,
                Prefixo = "P",
                Sufixo = "S",
                TipoEntradaID = (byte)eTipoEntrada.Numero_CaixaDeTexto,
                TipoValidadorID = eTipoValidador.Numero_MaiorIgualZero
            };

            repoPergunta.Insert(pIdade);

            var pCondicionalIdade = new PerguntaTexto
            {
                PerguntaID = 6,
                Descricao = "Cond",
                Titulo = "Teste Condicional",
                Obrigatorio = true,
                PatternRegex = null,
                TamanhoMaximo = 50,
                TipoEntradaID = (byte)eTipoEntrada.Texto_CaixaDeTexto,
                TipoValidadorID = null,
                ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                PerguntaCondicional = new PerguntaCondicionalNumero
                {
                    PerguntaCondicionalID = 10,
                    Operacao = eOperacaoCondicional.Numero_Igual,
                    Pergunta = pIdade,
                    PerguntaID = pIdade.PerguntaID,
                    ValorAtivacao = 20,
                    ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                },
                PerguntaCondicionalID = 10,

            };

            repoPerguntaCondicional.Insert(pCondicionalIdade.PerguntaCondicional);
            repoPergunta.Insert(pCondicionalIdade);

            var opcoesGrade = new HashSet<Opcao>
                               {
                                   new Opcao { Descricao="Básico", OpcaoID=21 ,ControleAtualizacao = ControleUsuario.Criar("ADMIN"),},
                                   new Opcao { Descricao="Intermediário", OpcaoID=22 ,ControleAtualizacao = ControleUsuario.Criar("ADMIN"),},
                                   new Opcao { Descricao="Avançado", OpcaoID=23,ControleAtualizacao = ControleUsuario.Criar("ADMIN"), }
                               };

            opcoesGrade.ToList().ForEach(c => repoOpcao.Insert(c));

            var pGrade = new PerguntaGradeDeOpcoes
            {
                PerguntaID = 7,
                Descricao = "AV",
                Obrigatorio = false,
                Titulo = "Avaliacao",
                PerguntaCondicional = null,
                Opcoes = opcoesGrade,
                TipoEntradaID = (byte)eTipoEntrada.Grade_Estrela,
                ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                TipoEntrada = new TipoEntrada { Descricao = "Estrela", TipoEntradaID = (byte)eTipoEntrada.Grade_Estrela, TipoPerguntaID = (byte)eTipoPergunta.Grade },
                Linhas = new HashSet<LinhaPerguntaGrade>
                     {
                          new LinhaPerguntaGrade
                          {

                               LinhaPerguntaGradeID = 1,
                              //PerguntaID = ++Identity,
                              //Descricao = null,
                              Titulo = "C#",
                              //Obrigatorio = false,
                               //Opcoes=opcoesGrade
                          },
                            new LinhaPerguntaGrade
                        {
                                LinhaPerguntaGradeID = 2,
                            //Descricao = null,
                            Titulo = "Java",
                            //Obrigatorio = false,
                            //Opcoes=opcoesGrade
                        }
                     }
            };

            pGrade.Linhas.ToList().ForEach(c => repoLinhaPerguntaGrade.Insert(c));
            repoPergunta.Insert(pGrade);

            ModeloDeFormulario modelo = new ModeloDeFormulario
            {
                ModeloFormularioID = 1,
                Descricao = "Modelo",
                Html = "<b>Modelo</b>",
                ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                Perguntas = new List<Pergunta>
                 {
                     pTexto,
                     pUnica,
                     pMultipla,
                     pUnicaCondicional,
                     pIdade,
                     pCondicionalIdade,
                     pGrade
                 }
            };

            repoModeloDeFormulario.Insert(modelo);

            modelo.AtribuirLeiautePerguntasPadrao();

            modelo.Perguntas.SelectMany(c => c.LeiautePerguntas).ToList().ForEach(c =>
            {
                repoLeiautePergunta.Insert(c);

                c.LeiauteItem.ToList().ForEach(d =>
                {
                    repoLeiautePerguntaItem.Insert(d);
                });
            });

            RespostaModeloDeFormulario respostaModelo = new RespostaModeloDeFormulario
            {
                ModeloDeFormulario = modelo,
                ModeloDeFormularioID = 1,
                RespostaModeloFormularioID = 1,
                ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                Respostas = new HashSet<Resposta>
                 {
                     new RespostaTexto
                     {
                          Pergunta = pTexto,
                           PerguntaID = pTexto.PerguntaID,
                          RespostaID=1,
                          Valor ="Hernandes",
                          ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                     },
                     new RespostaNumero
                     {
                          Pergunta=pIdade,
                           PerguntaID=pIdade.PerguntaID,
                           RespostaID=6,
                            Valor=20,
                            ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                     },
                     new RespostaUnica
                     {
                          RespostaID=2,
                           Pergunta=pUnica,
                            PerguntaID = pUnica.PerguntaID,
                            OpcaoEscolhida = pUnica.Opcoes.First(),
                            ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                     },
                     new RespostaMultipla
                     {
                         RespostaID=3,
                          Pergunta=pMultipla,
                          PerguntaID=pMultipla.PerguntaID,
                           OpcoesEscolhida = pMultipla.Opcoes.Skip(1).Take(2).Select(c=> new OpcaoRespondida { OpcaoID=c.OpcaoID }).ToList(),
                           ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                     },
                     //new RespostaUnica
                     //{
                     //     RespostaID=3,
                     //     Pergunta = pUnicaCondicional,
                     //     PerguntaID=pUnicaCondicional.PerguntaID,
                     //       //OpcaoEscolhida = pUnicaCondicional.Opcoes.First(),
                     //       // OpcaoEscolhidaID=pUnicaCondicional.Opcoes.First().OpcaoID,
                     //},
                     new RespostaTexto
                     {
                          Pergunta=pCondicionalIdade,
                          PerguntaID=pCondicionalIdade.PerguntaID,
                           Valor="A",
                           ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                     },
                     new RespostaGrade
                     {
                          RespostaID = 7,
                           Pergunta = pGrade,
                           PerguntaID=pGrade.PerguntaID,
                           ControleAtualizacao = ControleUsuario.Criar("ADMIN"),
                           Respostas = new List<RespostaLinhaPerguntaGrade>
                           {
                               new RespostaLinhaPerguntaGrade{ RespostaGradeID=1, LinhaPerguntaGradeID =1, OpcaoRespondidaID = 21},
                               new RespostaLinhaPerguntaGrade{ RespostaGradeID=1, LinhaPerguntaGradeID =2, OpcaoRespondidaID = 22}
                           }
                     }
                 }
            };

            respostaModelo.Validar();

            respostaModelo.Respostas.ToList().ForEach(c => repoResposta.Insert(c));
            repoRespostaModeloDeFormulario.Insert(respostaModelo);

            unit.Commit("ADMIN");
        }

    }
}
