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
    public class RespostaNumeroTests
    {
        [TestMethod()]
        public void AtribuirRespostaTest()
        {
            var resposta = new RespostaNumero
            {
            };


            resposta.AtribuirResposta(new DTO.RespostaDTO
            {
                PerguntaID = 1,
                Valor = 20,
            });

            Assert.AreEqual(1, resposta.PerguntaID);
            Assert.AreEqual(20, resposta.Valor);
        }

        [TestMethod()]
        public void ValidarTest()
        {
            var resposta = new RespostaNumero
            {
            };

            resposta.AtribuirResposta(new DTO.RespostaDTO
            {
                PerguntaID = 1,
                Valor = 1,
            });
            resposta.Pergunta = new PerguntaNumero
            {
                Obrigatorio = false
            };


            resposta.Pergunta.Obrigatorio = true;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = null;
            (resposta.Pergunta as PerguntaNumero).Obrigatorio = true;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Valor = 0;
            (resposta.Pergunta as PerguntaNumero).Obrigatorio = true;
            (resposta.Pergunta as PerguntaNumero).TipoValidadorID = eTipoValidador.Numero_MaiorIgualZero;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = -1;
            (resposta.Pergunta as PerguntaNumero).Obrigatorio = false;
            (resposta.Pergunta as PerguntaNumero).TipoValidadorID = eTipoValidador.Numero_MaiorIgualZero;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Valor = 1;
            (resposta.Pergunta as PerguntaNumero).Obrigatorio = true;
            (resposta.Pergunta as PerguntaNumero).TipoValidadorID = eTipoValidador.Numero_MaiorZero;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = 0;
            (resposta.Pergunta as PerguntaNumero).Obrigatorio = true;
            (resposta.Pergunta as PerguntaNumero).TipoValidadorID = eTipoValidador.Numero_MaiorZero;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Valor = -1;
            (resposta.Pergunta as PerguntaNumero).Obrigatorio = true;
            (resposta.Pergunta as PerguntaNumero).TipoValidadorID = eTipoValidador.Numero_MenorZero;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = 0;
            (resposta.Pergunta as PerguntaNumero).Obrigatorio = true;
            (resposta.Pergunta as PerguntaNumero).TipoValidadorID = eTipoValidador.Numero_MenorZero;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Valor = 0;
            (resposta.Pergunta as PerguntaNumero).Obrigatorio = true;
            (resposta.Pergunta as PerguntaNumero).TipoValidadorID = eTipoValidador.Numero_MenorIgualZero;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = 1;
            (resposta.Pergunta as PerguntaNumero).Obrigatorio = true;
            (resposta.Pergunta as PerguntaNumero).TipoValidadorID = eTipoValidador.Numero_MenorIgualZero;
            Assert.AreEqual(false, resposta.Validar());                       
        }
    }
}