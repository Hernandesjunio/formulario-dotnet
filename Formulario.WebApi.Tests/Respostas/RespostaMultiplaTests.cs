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
    public class RespostaMultiplaTests
    {
        [TestMethod()]
        public void AtribuirRespostaTest()
        {
            var resposta = new RespostaMultipla
            {
            };

            resposta.Pergunta = new Perguntas.PerguntaMultiplaEscolha
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
                Opcoes = new List<long> { 1, 2 }
            });

            Assert.AreEqual(1, resposta.PerguntaID);
            Assert.AreEqual(1, resposta.OpcoesEscolhida.First().OpcaoID);
            Assert.AreEqual(2, resposta.OpcoesEscolhida.Skip(1).First().OpcaoID);
        }

        [TestMethod()]
        public void ValidarTest()
        {
            var resposta = new RespostaMultipla
            {
            };

            resposta.Pergunta = new Perguntas.PerguntaMultiplaEscolha
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
                Opcoes = new List<long> { 1, 2 }
            });

            resposta.Pergunta.Obrigatorio = false;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Pergunta.Obrigatorio = true;
            Assert.AreEqual(true, resposta.Validar());

            resposta.OpcoesEscolhida.Clear();
            resposta.Pergunta.Obrigatorio = false;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Pergunta.Obrigatorio = true;
            Assert.AreEqual(false, resposta.Validar());
        }
    }
}