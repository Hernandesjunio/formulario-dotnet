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
    public class RespostaTextoTests
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
            var resposta = new RespostaTexto
            {
            };

            resposta.AtribuirResposta(new DTO.RespostaDTO
            {
                PerguntaID = 1,
                Valor = DateTime.Now.Date,                 
            });

            resposta.Pergunta = new PerguntaTexto
            {
                Obrigatorio = false,                 
            };
            
            resposta.Pergunta.Obrigatorio = true;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = null;
            (resposta.Pergunta as PerguntaTexto).Obrigatorio = true;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Valor = "12345678000195";
            (resposta.Pergunta as PerguntaTexto).Obrigatorio = true;
            (resposta.Pergunta as PerguntaTexto).TipoValidadorID = eTipoValidador.Texto_CNPJ;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = "12345678000180";
            (resposta.Pergunta as PerguntaTexto).Obrigatorio = true;
            (resposta.Pergunta as PerguntaTexto).TipoValidadorID = eTipoValidador.Texto_CNPJ;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Valor = "12312312387";
            (resposta.Pergunta as PerguntaTexto).Obrigatorio = true;
            (resposta.Pergunta as PerguntaTexto).TipoValidadorID = eTipoValidador.Texto_CPF;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = "12312312300";
            (resposta.Pergunta as PerguntaTexto).Obrigatorio = true;
            (resposta.Pergunta as PerguntaTexto).TipoValidadorID = eTipoValidador.Texto_CPF;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Valor = "asdf@3-asdf.com";
            (resposta.Pergunta as PerguntaTexto).Obrigatorio = true;
            (resposta.Pergunta as PerguntaTexto).TipoValidadorID = eTipoValidador.Texto_Email;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = "asdf@asdf";
            (resposta.Pergunta as PerguntaTexto).Obrigatorio = true;
            (resposta.Pergunta as PerguntaTexto).TipoValidadorID = eTipoValidador.Texto_Email;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Valor = "12345";
            (resposta.Pergunta as PerguntaTexto).Obrigatorio = true;
            (resposta.Pergunta as PerguntaTexto).PatternRegex = @"\d*";
            (resposta.Pergunta as PerguntaTexto).TipoValidadorID = eTipoValidador.Texto_Regex;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Valor = "asdf";
            (resposta.Pergunta as PerguntaTexto).Obrigatorio = true;
            (resposta.Pergunta as PerguntaTexto).PatternRegex = @"\d+";
            (resposta.Pergunta as PerguntaTexto).TipoValidadorID = eTipoValidador.Texto_Regex;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Valor = null;
            (resposta.Pergunta as PerguntaTexto).Obrigatorio = true;
            (resposta.Pergunta as PerguntaTexto).TipoValidadorID = null;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Valor = "";
            (resposta.Pergunta as PerguntaTexto).Obrigatorio = true;
            (resposta.Pergunta as PerguntaTexto).TipoValidadorID = null;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Valor = "    ";
            (resposta.Pergunta as PerguntaTexto).Obrigatorio = true;
            (resposta.Pergunta as PerguntaTexto).TipoValidadorID = null;
            Assert.AreEqual(false, resposta.Validar());                        
        }
    }
}