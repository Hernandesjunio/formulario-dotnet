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
    public class PerguntaCondicionalMultiplaTests
    {
        [TestMethod()]
        public void AtribuirCondicionalTest()
        {

            PerguntaMultiplaEscolha p = (PerguntaMultiplaEscolha)new PerguntaMultiplaEscolha().AtribuirPergunta(new DTO.PerguntaDTO
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

            PerguntaCondicionalMultipla pCondicionalMultipla = (PerguntaCondicionalMultipla)new PerguntaCondicionalMultipla().AtribuirCondicional(new PerguntaCondicionalDTO
            {
                OpcoesAtivacao = new List<long> { 1 },
                PerguntaCondicionalID = 1,
                PerguntaID = p.PerguntaID,
                OperacaoCondicional = (byte)eOperacaoCondicional.MultiplaOpcoes_Contem,
                PerguntasGrade = new List<long> { 1, 2, 3 }.ToDictionary(d => d, e => "Opcao_" + e.ToString()),
                TipoPergunta = eTipoPergunta.MultiplaEscolha,
                ValorAtivacao = 0
            }, p);

            Assert.AreEqual(1, pCondicionalMultipla.PerguntaCondicionalID);
            Assert.AreEqual(eOperacaoCondicional.MultiplaOpcoes_Contem, pCondicionalMultipla.Operacao);
            Assert.AreEqual(1, pCondicionalMultipla.OpcoesAtivacao.Count);                        
        }

        [TestMethod()]
        public void ValidarCondicionalTest()
        {
            PerguntaMultiplaEscolha p = (PerguntaMultiplaEscolha)new PerguntaMultiplaEscolha().AtribuirPergunta(new DTO.PerguntaDTO
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

            PerguntaCondicionalMultipla pCondicionalMultipla = (PerguntaCondicionalMultipla)new PerguntaCondicionalMultipla().AtribuirCondicional(new PerguntaCondicionalDTO
            {
                OpcoesAtivacao = new List<long> { 1 },
                PerguntaCondicionalID = 1,
                PerguntaID = p.PerguntaID,
                OperacaoCondicional = (byte)eOperacaoCondicional.MultiplaOpcoes_Contem,
                PerguntasGrade = new List<long> { 1, 2, 3 }.ToDictionary(d => d, e => "Opcao_" + e.ToString()),
                TipoPergunta = eTipoPergunta.MultiplaEscolha,
                ValorAtivacao = 0
            }, p);

            Assert.AreEqual(1, pCondicionalMultipla.PerguntaCondicionalID);
            Assert.AreEqual(eOperacaoCondicional.MultiplaOpcoes_Contem, pCondicionalMultipla.Operacao);
            Assert.AreEqual(1, pCondicionalMultipla.OpcoesAtivacao.Count);

            RespostaMultipla resposta = (RespostaMultipla)new RespostaMultipla().AtribuirResposta(new DTO.RespostaDTO
            {
                Opcoes = new List<long> { 1 },
                Valor = null,
                Extensao = "pdf",
                NomeArquivo = "Documento",
                UsuarioID = "admin"
            });

            Assert.AreEqual(true, pCondicionalMultipla.VerificarAtivacaoCondicional(resposta));

            pCondicionalMultipla.OpcoesAtivacao.Clear();
            pCondicionalMultipla.OpcoesAtivacao.Add(new Misc.OpcaoAtivacao { OpcaoID = 2 });
            Assert.AreEqual(false, pCondicionalMultipla.VerificarAtivacaoCondicional(resposta));
        }
    }
}