using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulario.Business.Respostas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.Business.Perguntas.Misc;

namespace Formulario.Business.Respostas.Tests
{
    [TestClass()]
    public class RespostaUnicaTests
    {
        [TestMethod()]
        public void AtribuirRespostaTest()
        {
            var resposta = new RespostaUnica
            {
            };

            resposta.Pergunta = new Perguntas.PerguntaEscolhaUnica
            {
                Opcoes = new List<Opcao>
                 {
                     new Opcao{ OpcaoID=1, Descricao="M" },
                     new Opcao{ OpcaoID=2, Descricao="F" }
                 }
            };

            resposta.AtribuirResposta(new DTO.RespostaDTO
            {
                PerguntaID = 1,
                OpcaoID = 1
            });

            Assert.AreEqual(1, resposta.PerguntaID);
            Assert.AreEqual(1, resposta.OpcaoEscolhidaID);
        }

        [TestMethod()]
        public void ValidarTest()
        {
            var resposta = new RespostaUnica
            {
            };

            resposta.Pergunta = new Perguntas.PerguntaEscolhaUnica
            {
                Opcoes = new List<Opcao>
                 {
                     new Opcao{ OpcaoID=1, Descricao="M" },
                     new Opcao{ OpcaoID=2, Descricao="F" }
                 }
            };

            resposta.AtribuirResposta(new DTO.RespostaDTO
            {
                PerguntaID = 1,
                OpcaoID = 1
            });

            Assert.AreEqual(1, resposta.PerguntaID);
            Assert.AreEqual(1, resposta.OpcaoEscolhidaID);
            
            resposta.Pergunta.Obrigatorio = false;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Pergunta.Obrigatorio = true;
            Assert.AreEqual(true, resposta.Validar());

            resposta.OpcaoEscolhidaID = null;
            resposta.Pergunta.Obrigatorio = false;
            Assert.AreEqual(true, resposta.Validar());

            resposta.OpcaoEscolhidaID = null;
            resposta.Pergunta.Obrigatorio = true;
            Assert.AreEqual(false, resposta.Validar());
        }
    }
}