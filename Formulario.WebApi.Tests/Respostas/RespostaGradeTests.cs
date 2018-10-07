using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulario.Business.Respostas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.Business.Perguntas.Misc;
using Formulario.Business.DTO;

namespace Formulario.Business.Respostas.Tests
{
    [TestClass()]
    public class RespostaGradeTests
    {
        [TestMethod()]
        public void AtribuirRespostaTest()
        {
            var resposta = new RespostaGrade
            {
            };

            resposta.Pergunta = new Perguntas.PerguntaGradeDeOpcoes
            {
                Opcoes = new List<Opcao>
                 {
                     new Opcao{ OpcaoID=1, Descricao="M" },
                     new Opcao{ OpcaoID=2, Descricao="F" }
                 },
                Linhas = new List<LinhaPerguntaGrade>
                {
                    new LinhaPerguntaGrade
                    {
                         LinhaPerguntaGradeID=1,
                          Titulo="Linha1"
                    },
                    new LinhaPerguntaGrade
                    {
                         LinhaPerguntaGradeID=2,
                          Titulo="Linha2"
                    }
                }
            };

            resposta.AtribuirResposta(new DTO.RespostaDTO
            {
                PerguntaID = 1,
                Opcoes = new List<long> { 1, 2 },
                RespostaGrade = new List<RespostaGradeDTO>
                 {
                     new RespostaGradeDTO
                     {
                           LinhaPerguntaGradeID =1,
                            OpcaoRespondidaID=2,
                     },
                     new RespostaGradeDTO
                     {
                           LinhaPerguntaGradeID =2,
                            OpcaoRespondidaID=1,
                     }
                 }
            });

            Assert.AreEqual(1, resposta.PerguntaID);

            Assert.AreEqual(2, resposta.Respostas.First().OpcaoRespondidaID);
            Assert.AreEqual(1, resposta.Respostas.First().LinhaPerguntaGradeID);
            Assert.AreEqual(1, resposta.Respostas.Skip(1).First().OpcaoRespondidaID);
            Assert.AreEqual(2, resposta.Respostas.Skip(1).First().LinhaPerguntaGradeID);
        }

        [TestMethod()]
        public void ValidarTest()
        {
            var resposta = new RespostaGrade
            {
            };

            resposta.Pergunta = new Perguntas.PerguntaGradeDeOpcoes
            {
                PerguntaID = 1,
                Linhas = new List<LinhaPerguntaGrade>
                 {
                      new LinhaPerguntaGrade{ Titulo="Conhecimento 1", LinhaPerguntaGradeID=1},
                      new LinhaPerguntaGrade{ Titulo="Conhecimento 2", LinhaPerguntaGradeID=2},
                 },
                Opcoes = new List<Opcao>
                 {
                     new Opcao{ OpcaoID=1, Descricao="Medio" },
                     new Opcao{ OpcaoID=2, Descricao="Fraco" }
                 }
            };

            resposta.AtribuirResposta(new DTO.RespostaDTO
            {
                PerguntaID = 1,
                Opcoes = new List<long> { 1, 2 },
                RespostaGrade = new List<RespostaGradeDTO>
                {
                     new RespostaGradeDTO{ LinhaPerguntaGradeID=1, OpcaoRespondidaID = 2},
                     new RespostaGradeDTO{ LinhaPerguntaGradeID=2, OpcaoRespondidaID = 2}
                }
            });

            Assert.AreEqual(2, resposta.Respostas.Count);

            resposta.Pergunta.Obrigatorio = false;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Pergunta.Obrigatorio = true;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Respostas.First().OpcaoRespondidaID = null;
            resposta.Pergunta.Obrigatorio = false;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Pergunta.Obrigatorio = true;
            Assert.AreEqual(false, resposta.Validar());
        }
    }
}