using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulario.Business.Perguntas.Concicional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.Business.DTO;
using Formulario.Business.Respostas;

namespace Formulario.Business.Perguntas.Concicional.Tests
{
    [TestClass()]
    public class PerguntaCondicionalDataTests
    {
        [TestMethod()]
        public void AtribuirCondicionalTest()
        {

            PerguntaData p = (PerguntaData)new PerguntaData().AtribuirPergunta(new DTO.PerguntaDTO
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
                TipoEntrada = (byte)eTipoEntrada.Texto_CaixaDeTexto,
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

            PerguntaCondicionalData pCondicionalData = (PerguntaCondicionalData)new PerguntaCondicionalData().AtribuirCondicional(new PerguntaCondicionalDTO
            {
                OpcoesAtivacao = new List<long> { 1, 2 },
                PerguntaCondicionalID = 1,
                PerguntaID = p.PerguntaID,
                OperacaoCondicional = (byte)eOperacaoCondicional.Data_Maior,
                PerguntasGrade = new List<long> { 1, 2, 3 }.ToDictionary(d => d, e => "Opcao_" + e.ToString()),
                TipoPergunta = eTipoPergunta.Data,
                ValorAtivacao = DateTime.Now.AddDays(2).Date
            }, p);

            Assert.AreEqual(1, pCondicionalData.PerguntaCondicionalID);
            Assert.AreEqual(eOperacaoCondicional.Data_Maior, pCondicionalData.Operacao);
            Assert.AreEqual(DateTime.Now.AddDays(2).Date, pCondicionalData.ValorAtivacao);
        }

        [TestMethod()]
        public void ValidarCondicionalTest()
        {
            PerguntaData p = (PerguntaData)new PerguntaData().AtribuirPergunta(new DTO.PerguntaDTO
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
                TipoEntrada = (byte)eTipoEntrada.Texto_CaixaDeTexto,
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

            PerguntaCondicionalData pCondicionalData = (PerguntaCondicionalData)new PerguntaCondicionalData().AtribuirCondicional(new PerguntaCondicionalDTO
            {
                OpcoesAtivacao = new List<long> { 1, 2 },
                PerguntaCondicionalID = 1,
                PerguntaID = p.PerguntaID,
                OperacaoCondicional = (byte)eOperacaoCondicional.Data_Maior,
                PerguntasGrade = new List<long> { 1, 2, 3 }.ToDictionary(d => d, e => "Opcao_" + e.ToString()),
                TipoPergunta = eTipoPergunta.Data,
                ValorAtivacao = DateTime.Now.AddDays(2).Date
            }, p);

            Assert.AreEqual(1, pCondicionalData.PerguntaCondicionalID);
            Assert.AreEqual(eOperacaoCondicional.Data_Maior, pCondicionalData.Operacao);
            Assert.AreEqual(DateTime.Now.AddDays(2).Date, pCondicionalData.ValorAtivacao);

            RespostaData resposta = (RespostaData)new RespostaData().AtribuirResposta(new DTO.RespostaDTO
            {
                Valor = DateTime.Now.AddDays(1).Date,
                Extensao = "pdf",
                NomeArquivo = "Documento",
                UsuarioID = "admin"
            });

            Assert.AreEqual(false, pCondicionalData.VerificarAtivacaoCondicional(resposta));

            pCondicionalData.ValorAtivacao = DateTime.Now.Date;
            Assert.AreEqual(true, pCondicionalData.VerificarAtivacaoCondicional(resposta));
        }
    }
}