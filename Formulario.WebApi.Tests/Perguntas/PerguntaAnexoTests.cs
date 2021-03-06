﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulario.Business.Perguntas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.Business.DTO;

namespace Formulario.Business.Perguntas.Tests
{
    [TestClass()]
    public class PerguntaAnexoTests
    {
        [TestMethod()]
        public void AtribuirPerguntaTest()
        {
            PerguntaAnexo p = (PerguntaAnexo)new PerguntaAnexo().AtribuirPergunta(new DTO.PerguntaDTO
            {
                CasasDecimais = 0,
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
                TipoEntrada = (byte)eTipoEntrada.Anexo_FileInput,
                TipoPergunta = eTipoPergunta.Texto,
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
            Assert.AreEqual(100, p.TamanhoMaximoBytes);
            Assert.IsNull(p.TipoValidadorID);
            Assert.AreEqual((byte)eTipoEntrada.Anexo_FileInput, p.TipoEntradaID);
        }
    }
}