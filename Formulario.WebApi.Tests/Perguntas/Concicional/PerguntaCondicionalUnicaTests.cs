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
    public class PerguntaCondicionalUnicaTests
    {
        [TestMethod()]
        public void AtribuirCondicionalTest()
        {

            PerguntaEscolhaUnica p = (PerguntaEscolhaUnica)new PerguntaEscolhaUnica().AtribuirPergunta(new DTO.PerguntaDTO
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
                Validador = (byte)eTipoValidador.Texto_Regex,
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

            PerguntaCondicionalUnica pCondicionalUnica = (PerguntaCondicionalUnica)new PerguntaCondicionalUnica().AtribuirCondicional(new PerguntaCondicionalDTO
            {
                OpcoesAtivacao = new List<long> { 1 },
                PerguntaCondicionalID = 1,
                PerguntaID = p.PerguntaID,
                OperacaoCondicional = (byte)eOperacaoCondicional.UnicaOpcao_Igual,
                PerguntasGrade = new List<long> { 1, 2, 3 }.ToDictionary(d => d, e => "Opcao_" + e.ToString()),
                TipoPergunta = eTipoPergunta.EscolhaUnica,
                ValorAtivacao = 1
            }, p);

            Assert.AreEqual(1, pCondicionalUnica.PerguntaCondicionalID);
            Assert.AreEqual(eOperacaoCondicional.UnicaOpcao_Igual, pCondicionalUnica.Operacao);
            Assert.AreEqual(1, pCondicionalUnica.OpcaoAtivacaoID);
        }

        [TestMethod()]
        public void ValidarCondicionalTest()
        {
            PerguntaEscolhaUnica p = (PerguntaEscolhaUnica)new PerguntaEscolhaUnica().AtribuirPergunta(new DTO.PerguntaDTO
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
                Validador = (byte)eTipoValidador.Texto_Regex,
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

            PerguntaCondicionalUnica pCondicionalUnica = (PerguntaCondicionalUnica)new PerguntaCondicionalUnica().AtribuirCondicional(new PerguntaCondicionalDTO
            {
                OpcoesAtivacao = new List<long> { 1 },
                PerguntaCondicionalID = 1,
                PerguntaID = p.PerguntaID,
                OperacaoCondicional = (byte)eOperacaoCondicional.UnicaOpcao_Igual,
                PerguntasGrade = new List<long> { 1, 2, 3 }.ToDictionary(d => d, e => "Opcao_" + e.ToString()),
                TipoPergunta = eTipoPergunta.EscolhaUnica,
                ValorAtivacao = 1
            }, p);

            Assert.AreEqual(1, pCondicionalUnica.PerguntaCondicionalID);
            Assert.AreEqual(eOperacaoCondicional.UnicaOpcao_Igual, pCondicionalUnica.Operacao);
            Assert.AreEqual(1, pCondicionalUnica.OpcaoAtivacaoID);

            RespostaUnica resposta = (RespostaUnica)new RespostaUnica().AtribuirResposta(new DTO.RespostaDTO
            {
                Opcoes = new List<long> { 1 },
                Valor = 1,
                Extensao = "pdf",
                NomeArquivo = "Documento",
                UsuarioID = "admin",
                OpcaoID = 1,
            });

            Assert.AreEqual(true, pCondicionalUnica.VerificarAtivacaoCondicional(resposta));

            resposta.OpcaoEscolhidaID = 0;
            Assert.AreEqual(false, pCondicionalUnica.VerificarAtivacaoCondicional(resposta));
        }
    }
}