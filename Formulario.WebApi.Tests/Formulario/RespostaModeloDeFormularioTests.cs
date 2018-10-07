using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulario.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.Business.Perguntas;
using Formulario.Business.Perguntas.Misc;
using Formulario.Business.Perguntas.Concicional;
using Formulario.Business.Respostas;

namespace Formulario.Business.Tests
{
    [TestClass()]
    public class RespostaModeloDeFormularioTests
    {
        [TestMethod()]
        public void ValidarTest()
        {
            long Identity = 0;
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
                    Operacao = eOperacaoCondicional.MultiplaOpcoes_Contem,
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

            modelo.AtribuirLeiautePerguntasPadrao();

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
                          Valor ="João"
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