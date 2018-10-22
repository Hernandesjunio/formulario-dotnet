using Formulario.Business.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Formulario.Business.Perguntas.Tests
{
    [TestClass()]
    public class PerguntaNumeroTests
    {
        [TestMethod()]
        public void AtribuirPerguntaTest()
        {
            PerguntaNumero p = (PerguntaNumero)new PerguntaNumero().AtribuirPergunta(new DTO.PerguntaDTO
            {
                CasasDecimais = 2,
                Deleted = false,
                Descricao = "Desc",
                Obrigatorio = true,
                PatternRegex = @"\w*",
                PerguntaID = 1,
                PerguntaCondicionalID = null,
                PerguntaCondicional = null,
                Prefixo = "R$",
                Sufixo = "%",
                TamanhoMaximo = 8,
                TipoEntrada = (byte)eTipoEntrada.Numero_ComBotoes,
                TipoPergunta = eTipoPergunta.Numero,
                TamanhoMaximoBytes = 100,
                Titulo = "Titulo",
                UsuarioID = "ADMIN",
                ValidadorID = (byte)eTipoValidador.Texto_Regex,
                Opcoes = new List<OpcaoDTO>
                 {
                     new OpcaoDTO{ OpcaoID=1, Descricao="M" },
                     new OpcaoDTO{ OpcaoID=2, Descricao="F" }
                 },
                LinhasGrade = new List<DTO.LinhasGradeDTO> { new DTO.LinhasGradeDTO
                 {
                      LinhaID=1,
                      Descricao="Nível"
                 },
                new DTO.LinhasGradeDTO{
                     LinhaID=2,
                      Descricao="SubNível"
                }
                }
            });

            Assert.AreEqual("Desc", p.Descricao);
            Assert.AreEqual("Titulo", p.Titulo);
            Assert.AreEqual(DateTime.Now.Date, p.ControleAtualizacao.Data.Date);
            Assert.AreEqual("ADMIN", p.ControleAtualizacao.UsuarioID);
            Assert.AreEqual("R$", p.Prefixo);
            Assert.AreEqual("%", p.Sufixo);
            Assert.AreEqual(2, p.CasasDecimais);
            Assert.AreEqual((byte)eTipoEntrada.Numero_ComBotoes, p.TipoEntradaID);
        }
    }
}