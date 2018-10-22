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
    public class PerguntaCondicionalNumeroTests
    {
        [TestMethod()]
        public void AtribuirCondicionalTest()
        {

            PerguntaNumero p = (PerguntaNumero)new PerguntaNumero().AtribuirPergunta(new DTO.PerguntaDTO
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

            PerguntaCondicionalNumero pCondicionalNumero = (PerguntaCondicionalNumero)new PerguntaCondicionalNumero().AtribuirCondicional(new PerguntaCondicionalDTO
            {
                OpcoesAtivacao = new List<long> { 1, 2 },
                PerguntaCondicionalID = 1,
                PerguntaID = p.PerguntaID,
                OperacaoCondicional = (byte)eOperacaoCondicional.Numero_Maior,
                PerguntasGrade = new List<long> { 1, 2, 3 }.ToDictionary(d => d, e => "Opcao_" + e.ToString()),
                TipoPergunta = eTipoPergunta.Numero,
                ValorAtivacao = 1.23
            }, p);

            Assert.AreEqual(1, pCondicionalNumero.PerguntaCondicionalID);
            Assert.AreEqual(eOperacaoCondicional.Numero_Maior, pCondicionalNumero.Operacao);
            Assert.AreEqual(1.23M, pCondicionalNumero.ValorAtivacao);
        }

        [TestMethod()]
        public void ValidarCondicionalTest()
        {
            PerguntaNumero p = (PerguntaNumero)new PerguntaNumero().AtribuirPergunta(new DTO.PerguntaDTO
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

            PerguntaCondicionalNumero pCondicionalNumero = (PerguntaCondicionalNumero)new PerguntaCondicionalNumero().AtribuirCondicional(new PerguntaCondicionalDTO
            {
                OpcoesAtivacao = new List<long> { 1, 2 },
                PerguntaCondicionalID = 1,
                PerguntaID = p.PerguntaID,
                OperacaoCondicional = (byte)eOperacaoCondicional.Numero_Maior,
                PerguntasGrade = new List<long> { 1, 2, 3 }.ToDictionary(d => d, e => "Opcao_" + e.ToString()),
                TipoPergunta = eTipoPergunta.Numero,
                ValorAtivacao = 1.23
            }, p);

            Assert.AreEqual(1, pCondicionalNumero.PerguntaCondicionalID);
            Assert.AreEqual(eOperacaoCondicional.Numero_Maior, pCondicionalNumero.Operacao);
            Assert.AreEqual(1.23M, pCondicionalNumero.ValorAtivacao);

            RespostaNumero resposta = (RespostaNumero)new RespostaNumero().AtribuirResposta(new DTO.RespostaDTO
            {
                Valor = 1.24,
                Extensao = "pdf",
                NomeArquivo = "Documento",
                UsuarioID = "admin"
            });

            Assert.AreEqual(true, pCondicionalNumero.VerificarAtivacaoCondicional(resposta));

            pCondicionalNumero.ValorAtivacao = 1000;
            Assert.AreEqual(false, pCondicionalNumero.VerificarAtivacaoCondicional(resposta));
        }
    }
}