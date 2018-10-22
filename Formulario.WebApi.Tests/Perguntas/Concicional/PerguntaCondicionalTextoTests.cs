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
    public class PerguntaCondicionalTextoTests
    {
        [TestMethod()]
        public void AtribuirCondicionalTest()
        {

            PerguntaTexto p = (PerguntaTexto)new PerguntaTexto().AtribuirPergunta(new DTO.PerguntaDTO
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

            PerguntaCondicionalTexto pCondicionalTexto = (PerguntaCondicionalTexto)new PerguntaCondicionalTexto().AtribuirCondicional(new PerguntaCondicionalDTO
            {
                OpcoesAtivacao = new List<long> { 1, 2 },
                PerguntaCondicionalID = 1,
                PerguntaID = p.PerguntaID,
                OperacaoCondicional = (byte)eOperacaoCondicional.Texto_Contem,
                PerguntasGrade = new List<long> { 1, 2, 3 }.ToDictionary(d => d, e => "Opcao_" + e.ToString()),
                TipoPergunta = eTipoPergunta.Texto,
                ValorAtivacao = "asdf"
            }, p);

            Assert.AreEqual(1, pCondicionalTexto.PerguntaCondicionalID);
            Assert.AreEqual(eOperacaoCondicional.Texto_Contem, pCondicionalTexto.Operacao);
            Assert.AreEqual("asdf", pCondicionalTexto.ValorAtivacao);
        }

        [TestMethod()]
        public void ValidarCondicionalTest()
        {
            PerguntaTexto p = (PerguntaTexto)new PerguntaTexto().AtribuirPergunta(new DTO.PerguntaDTO
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

            PerguntaCondicionalTexto pCondicionalTexto = (PerguntaCondicionalTexto)new PerguntaCondicionalTexto().AtribuirCondicional(new PerguntaCondicionalDTO
            {
                OpcoesAtivacao = new List<long> { 1, 2 },
                PerguntaCondicionalID = 1,
                PerguntaID = p.PerguntaID,
                OperacaoCondicional = (byte)eOperacaoCondicional.Texto_Contem,
                PerguntasGrade = new List<long> { 1, 2, 3 }.ToDictionary(d => d, e => "Opcao_" + e.ToString()),
                TipoPergunta = eTipoPergunta.Texto,
                ValorAtivacao = "asdf"
            }, p);

            Assert.AreEqual(1, pCondicionalTexto.PerguntaCondicionalID);
            Assert.AreEqual(eOperacaoCondicional.Texto_Contem, pCondicionalTexto.Operacao);
            Assert.AreEqual("asdf", pCondicionalTexto.ValorAtivacao);

            RespostaTexto resposta = (RespostaTexto)new RespostaTexto().AtribuirResposta(new DTO.RespostaDTO
            {
                Valor = "asdf",
                Extensao = "pdf",
                NomeArquivo = "Documento",
                UsuarioID = "admin"
            });

            Assert.AreEqual(true, pCondicionalTexto.VerificarAtivacaoCondicional(resposta));

            pCondicionalTexto.ValorAtivacao = "qwer";
            Assert.AreEqual(false, pCondicionalTexto.VerificarAtivacaoCondicional(resposta));
        }
    }
}