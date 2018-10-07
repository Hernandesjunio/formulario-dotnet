using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulario.Business.Respostas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.Business.Perguntas;

namespace Formulario.Business.Respostas.Tests
{
    [TestClass()]
    public class RespostaDataTests
    {
        [TestMethod()]
        public void AtribuirRespostaTest()
        {
            var resposta = new RespostaData
            {
            };


            resposta.AtribuirResposta(new DTO.RespostaDTO
            {
                PerguntaID = 1,
                Valor = DateTime.Now.Date,
            });

            Assert.AreEqual(1, resposta.PerguntaID);
            Assert.AreEqual(DateTime.Now.Date, resposta.Valor);
        }

        [TestMethod()]
        public void ValidarTest()
        {
            var resposta = new RespostaData
            {
            };

            resposta.AtribuirResposta(new DTO.RespostaDTO
            {
                PerguntaID = 1,
                Valor = DateTime.Now.Date,
            });
            resposta.Pergunta = new PerguntaData
            {
                Obrigatorio = false
            };


            resposta.Pergunta.Obrigatorio = true;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = null;
            (resposta.Pergunta as PerguntaData).Obrigatorio = true;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Valor = DateTime.Now.AddDays(1);
            (resposta.Pergunta as PerguntaData).Obrigatorio = true;
            (resposta.Pergunta as PerguntaData).TipoValidadorID = eTipoValidador.Data_QualquerValor;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = DateTime.Now.AddDays(1);
            (resposta.Pergunta as PerguntaData).Obrigatorio = false;
            (resposta.Pergunta as PerguntaData).TipoValidadorID = eTipoValidador.Data_QualquerValor;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = DateTime.Now.AddDays(1);
            (resposta.Pergunta as PerguntaData).Obrigatorio = true;
            (resposta.Pergunta as PerguntaData).TipoValidadorID = eTipoValidador.Data_SomenteMaiorHoje;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = DateTime.Now.Date;
            (resposta.Pergunta as PerguntaData).Obrigatorio = true;
            (resposta.Pergunta as PerguntaData).TipoValidadorID = eTipoValidador.Data_SomenteMaiorHoje;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Valor = DateTime.Now;
            (resposta.Pergunta as PerguntaData).Obrigatorio = true;
            (resposta.Pergunta as PerguntaData).TipoValidadorID = eTipoValidador.Data_SomenteMaiorIgualHoje;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = DateTime.Now.AddDays(-1);
            (resposta.Pergunta as PerguntaData).Obrigatorio = true;
            (resposta.Pergunta as PerguntaData).TipoValidadorID = eTipoValidador.Data_SomenteMaiorIgualHoje;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Valor = DateTime.Now.AddDays(-1);
            (resposta.Pergunta as PerguntaData).Obrigatorio = true;
            (resposta.Pergunta as PerguntaData).TipoValidadorID = eTipoValidador.Data_SomenteMenorHoje;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = DateTime.Now;
            (resposta.Pergunta as PerguntaData).Obrigatorio = true;
            (resposta.Pergunta as PerguntaData).TipoValidadorID = eTipoValidador.Data_SomenteMenorHoje;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Valor = DateTime.Now;
            (resposta.Pergunta as PerguntaData).Obrigatorio = true;
            (resposta.Pergunta as PerguntaData).TipoValidadorID = eTipoValidador.Data_SomenteMenorIgualHoje;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = DateTime.Now.AddDays(1);
            (resposta.Pergunta as PerguntaData).Obrigatorio = true;
            (resposta.Pergunta as PerguntaData).TipoValidadorID = eTipoValidador.Data_SomenteMenorIgualHoje;
            Assert.AreEqual(false, resposta.Validar());


        }
    }
}