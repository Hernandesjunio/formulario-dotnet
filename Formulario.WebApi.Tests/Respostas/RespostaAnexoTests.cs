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
    public class RespostaAnexoTests
    {
        [TestMethod()]
        public void AtribuirRespostaTest()
        {
            RespostaAnexo resposta = new RespostaAnexo
            {
            };

            var base64 = Convert.ToBase64String(new byte[10]);

            resposta.AtribuirResposta(new DTO.RespostaDTO
            {
                Valor = base64,
                Extensao = "pdf",
                NomeArquivo = "Documento",
                UsuarioID = "admin"
            });

            Assert.AreEqual("pdf", resposta.Valor.Extensao);
            Assert.AreEqual("Documento", resposta.Valor.Nome);
            Assert.AreEqual("admin", resposta.Valor.ControleUsuario.UsuarioID);
            Assert.AreEqual(Convert.ToBase64String(new byte[10]), Convert.ToBase64String(resposta.Valor.Conteudo));
        }

        [TestMethod()]
        public void ValidarTest()
        {
            var base64 = Convert.ToBase64String(new byte[10]);
            RespostaAnexo resposta = new RespostaAnexo();

            resposta.AtribuirResposta(new DTO.RespostaDTO
            {
                Valor = base64,
                Extensao = "pdf",
                NomeArquivo = "Documento",
                UsuarioID = "admin"
            });

            resposta.Pergunta = new PerguntaAnexo
            {
                Obrigatorio = false
            };
            Assert.AreEqual(true, resposta.Validar());

            resposta.Pergunta.Obrigatorio = true;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Pergunta.Obrigatorio = true;
            resposta.Valor.Conteudo = new byte[0];
            Assert.AreEqual(false, resposta.Validar());

            resposta.Pergunta.Obrigatorio = true;
            resposta.Valor.Conteudo = null;
            Assert.AreEqual(false, resposta.Validar());

            resposta.Pergunta.Obrigatorio = false;
            resposta.Valor.Conteudo = null;
            Assert.AreEqual(true, resposta.Validar());

            resposta.Pergunta.Obrigatorio = false;
            resposta.Valor.Conteudo = new byte[1];
            Assert.AreEqual(true, resposta.Validar());

            resposta.Pergunta.Obrigatorio = false;
            (resposta.Pergunta as PerguntaAnexo).TamanhoMaximoBytes = 1;
            resposta.Valor.Conteudo = new byte[10];
            Assert.AreEqual(false, resposta.Validar());
                        
            (resposta.Pergunta as PerguntaAnexo).TamanhoMaximoBytes = 1;
            resposta.Valor.Conteudo = null;
            resposta.Pergunta.Obrigatorio = true;
            Assert.AreEqual(false, resposta.Validar());

        }
    }
}