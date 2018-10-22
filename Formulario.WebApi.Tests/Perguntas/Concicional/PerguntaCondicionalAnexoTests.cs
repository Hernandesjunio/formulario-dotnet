using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulario.Business.Perguntas.Concicional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.ComplexProperties;
using Formulario.Business.DTO;
using Formulario.Business.Respostas;

namespace Formulario.Business.Perguntas.Concicional.Tests
{
    [TestClass()]
    public class PerguntaCondicionalAnexoTests
    {
        [TestMethod()]
        public void AtribuirCondicionalTest()
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

            PerguntaCondicionalAnexo pCondicionalAnexo = (PerguntaCondicionalAnexo)new PerguntaCondicionalAnexo().AtribuirCondicional(new PerguntaCondicionalDTO
            {
                OpcoesAtivacao = new List<long> { 1, 2 },
                PerguntaCondicionalID = 1,
                PerguntaID = p.PerguntaID,
                OperacaoCondicional = (byte)eOperacaoCondicional.Anexo_Maior,
                PerguntasGrade = new List<long> { 1, 2, 3 }.ToDictionary(d => d, e => "Opcao_" + e.ToString()),
                TipoPergunta = eTipoPergunta.Anexo,
                ValorAtivacao = 0
            }, p);

            Assert.AreEqual(1, pCondicionalAnexo.PerguntaCondicionalID);
            Assert.AreEqual(eOperacaoCondicional.Anexo_Maior, pCondicionalAnexo.Operacao);
            Assert.AreEqual(0, pCondicionalAnexo.ValorAtivacao);

            var base64 = Convert.ToBase64String(new byte[10]);

            RespostaAnexo resposta = (RespostaAnexo)new RespostaAnexo().AtribuirResposta(new DTO.RespostaDTO
            {
                Valor = base64,
                Extensao = "pdf",
                NomeArquivo = "Documento",
                UsuarioID = "admin"
            });

            Assert.AreEqual(true, pCondicionalAnexo.VerificarAtivacaoCondicional(resposta));

            pCondicionalAnexo.ValorAtivacao = 1000;
            Assert.AreEqual(false, pCondicionalAnexo.VerificarAtivacaoCondicional(resposta));
                        
        }

        [TestMethod()]
        public void ValidarCondicionalTest()
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

            PerguntaCondicionalAnexo pCondicionalAnexo = (PerguntaCondicionalAnexo)new PerguntaCondicionalAnexo().AtribuirCondicional(new PerguntaCondicionalDTO
            {
                OpcoesAtivacao = new List<long> { 1, 2 },
                PerguntaCondicionalID = 1,
                PerguntaID = p.PerguntaID,
                OperacaoCondicional = (byte)eOperacaoCondicional.Anexo_Maior,
                PerguntasGrade = new List<long> { 1, 2, 3 }.ToDictionary(d => d, e => "Opcao_" + e.ToString()),
                TipoPergunta = eTipoPergunta.Anexo,
                ValorAtivacao = 0
            }, p);

            Assert.AreEqual(1, pCondicionalAnexo.PerguntaCondicionalID);
            Assert.AreEqual(eOperacaoCondicional.Anexo_Maior, pCondicionalAnexo.Operacao);
            Assert.AreEqual(0, pCondicionalAnexo.ValorAtivacao);

            var base64 = Convert.ToBase64String(new byte[9]);

            RespostaAnexo resposta = (RespostaAnexo)new RespostaAnexo().AtribuirResposta(new DTO.RespostaDTO
            {
                Valor = base64,
                Extensao = "pdf",
                NomeArquivo = "Documento",
                UsuarioID = "admin"
            });

            Assert.AreEqual(true, pCondicionalAnexo.VerificarAtivacaoCondicional(resposta));
            pCondicionalAnexo.Operacao = eOperacaoCondicional.Anexo_Menor;
            Assert.AreEqual(false, pCondicionalAnexo.VerificarAtivacaoCondicional(resposta));
        }
    }
}