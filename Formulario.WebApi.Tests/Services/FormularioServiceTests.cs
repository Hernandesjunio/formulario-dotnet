using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulario.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Formulario.Business.RepositoryPattern;
using Formulario.Business.Perguntas;
using Formulario.WebApi.Tests.MockData;
using Formulario.Business.DTO;
using Formulario.Business.Perguntas.Misc;
using Formulario.Business.Respostas;
using Formulario.Business.Perguntas.Concicional;
using Formulario.ComplexProperties;

namespace Formulario.Business.Tests
{
    [TestClass()]
    public class FormularioServiceTests
    {
        IFormularioService svc;
        IDatabaseFactory factory;
        IUnitOfWOrkGeneric<IDatabaseFactory> unit;

        [TestCleanup]
        public void Clear()
        {
            factory.Dispose();
            svc.Dispose();
        }

        [TestInitialize]
        public void Init()
        {
            factory = new DatabaseFactoryMock();
            unit = new UnitOfWorkMock<IDatabaseFactory>(factory);
            svc = new FormularioService(factory, unit);
            DatabaseFactoryMock.CreateData(svc, factory, unit);
        }

        [TestMethod()]
        public void BuscarModeloDeFormularioTest()
        {
            var result = svc.BuscarModeloDeFormulario().SingleOrDefault(d => d.ModeloFormularioID == 1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Modelo", result.Descricao);
            Assert.AreEqual("<b>Modelo</b>", result.Html);
            Assert.AreEqual("ADMIN", result.ControleAtualizacao.UsuarioID);
            Assert.AreEqual(DateTime.Now.Date, result.ControleAtualizacao.Data.Date);
        }

        [TestMethod()]
        public void BuscarRespostaModeloDeFormularioTest()
        {
            var result = svc.BuscarRespostaModeloDeFormulario();
            Assert.IsTrue(result.Any());
        }

        [TestMethod()]
        public void AbrirFormularioTest()
        {
            var formulario = svc.AbrirFormulario(1);

            Assert.AreEqual(1, formulario.ModeloFormularioID);
            Assert.AreEqual("Modelo", formulario.Descricao);
            Assert.AreEqual("<b>Modelo</b>", formulario.Html);

            var p1 = formulario.Perguntas.Single(c => c.PerguntaID == 1);
            Assert.AreEqual("Linguagem", p1.Titulo);
            Assert.AreEqual("Ling", p1.Descricao);
            Assert.AreEqual(false, p1.Obrigatorio);
            Assert.AreEqual((byte)eTipoEntrada.MultiplaEscolha_CaixaDeSelecao, p1.TipoEntrada);
            Assert.AreEqual(4, p1.Opcoes.Count);
            Assert.AreEqual("C#", p1.Opcoes[0].Descricao);
            Assert.AreEqual("JAVA", p1.Opcoes[1].Descricao);
            Assert.AreEqual("JavaScript", p1.Opcoes[2].Descricao);
            Assert.AreEqual("Python", p1.Opcoes[3].Descricao);

            var p2 = formulario.Perguntas.Single(c => c.PerguntaID == 2);
            Assert.AreEqual("Nome", p2.Titulo);
            Assert.AreEqual("Desc", p2.Descricao);
            Assert.AreEqual(true, p2.Obrigatorio);
            Assert.AreEqual((short)50, p2.TamanhoMaximo);
            Assert.AreEqual((byte)eTipoEntrada.Texto_CaixaDeTexto, p2.TipoEntrada);

            var p3 = formulario.Perguntas.Single(c => c.PerguntaID == 3);
            Assert.AreEqual("Sexo", p3.Titulo);
            Assert.AreEqual("Desc Sexo", p3.Descricao);
            Assert.AreEqual(false, p3.Obrigatorio);
            Assert.AreEqual((byte)eTipoEntrada.EscolhaUnica_Radio, p3.TipoEntrada);
            Assert.AreEqual(2, p3.Opcoes.Count);
            Assert.AreEqual("M", p3.Opcoes[0].Descricao);
            Assert.AreEqual("F", p3.Opcoes[1].Descricao);

            var p4 = formulario.Perguntas.Single(c => c.PerguntaID == 4);
            Assert.AreEqual("Nível de Conhecimento C#", p4.Titulo);
            Assert.AreEqual("Nível", p4.Descricao);
            Assert.AreEqual(false, p4.Obrigatorio);
            Assert.AreEqual((byte)eTipoEntrada.EscolhaUnica_Star, p4.TipoEntrada);
            Assert.AreEqual(3, p4.Opcoes.Count);
            Assert.AreEqual("Básico", p4.Opcoes[0].Descricao);
            Assert.AreEqual("Médio", p4.Opcoes[1].Descricao);
            Assert.AreEqual("Avançado", p4.Opcoes[2].Descricao);
            Assert.IsNotNull(p4.PerguntaCondicional);
            //Assert.IsNotNull(p4.PerguntaCondicional.PerguntaID)
            Assert.AreEqual(eTipoPergunta.MultiplaEscolha, p4.PerguntaCondicional.TipoPergunta);
            Assert.AreEqual(1, p4.PerguntaCondicional.OpcoesAtivacao.Count);
            Assert.AreEqual(1, p4.PerguntaCondicional.OpcoesAtivacao.Single());
            Assert.AreEqual((short)eOperacaoCondicional.MultiplaOpcoes_Contem, p4.PerguntaCondicional.OperacaoCondicional);

            var p5 = formulario.Perguntas.Single(c => c.PerguntaID == 5);
            Assert.AreEqual("Idade", p5.Titulo);
            Assert.AreEqual("I", p5.Descricao);
            Assert.AreEqual((byte)0, p5.CasasDecimais);
            Assert.AreEqual("P", p5.Prefixo);
            Assert.AreEqual("S", p5.Sufixo);
            Assert.AreEqual(true, p5.Obrigatorio);
            Assert.AreEqual((byte)eTipoEntrada.Numero_CaixaDeTexto, p5.TipoEntrada);
            Assert.AreEqual((short?)eTipoValidador.Numero_MaiorIgualZero, p5.ValidadorID);

            var p6 = formulario.Perguntas.Single(c => c.PerguntaID == 6);
            Assert.AreEqual("Teste Condicional", p6.Titulo);
            Assert.AreEqual("Cond", p6.Descricao);
            Assert.AreEqual(true, p6.Obrigatorio);
            Assert.AreEqual((short)50, p6.TamanhoMaximo);
            Assert.AreEqual((byte)eTipoEntrada.Texto_CaixaDeTexto, p6.TipoEntrada);
            Assert.IsNotNull(p6.PerguntaCondicional);
            Assert.AreEqual(eTipoPergunta.Numero, p6.PerguntaCondicional.TipoPergunta);
            Assert.AreEqual(20M, p6.PerguntaCondicional.ValorAtivacao);
            Assert.AreEqual((short)eOperacaoCondicional.Numero_Igual, p6.PerguntaCondicional.OperacaoCondicional);

            var p7 = formulario.Perguntas.Single(c => c.PerguntaID == 7);
            Assert.AreEqual("Avaliacao", p7.Titulo);
            Assert.AreEqual("AV", p7.Descricao);
            Assert.AreEqual(false, p7.Obrigatorio);
            Assert.AreEqual((byte)eTipoEntrada.Grade_Estrela, p7.TipoEntrada);
            Assert.AreEqual(3, p7.Opcoes.Count);
            Assert.AreEqual("Básico", p7.Opcoes[0].Descricao);
            Assert.AreEqual("Intermediário", p7.Opcoes[1].Descricao);
            Assert.AreEqual("Avançado", p7.Opcoes[2].Descricao);
            Assert.AreEqual(2, p7.LinhasGrade.Count);
            Assert.AreEqual("C#", p7.LinhasGrade[0].Descricao);
            Assert.AreEqual("Java", p7.LinhasGrade[1].Descricao);
            Assert.IsNull(p7.PerguntaCondicional);

            string serializado = Newtonsoft.Json.JsonConvert.SerializeObject(formulario, Newtonsoft.Json.Formatting.None, 
                new Newtonsoft.Json.JsonSerializerSettings
            {
                  
            }
            );
        }

        [TestMethod()]
        public void AbrirRespostaModeloFormularioTest()
        {
            var resposta = svc.AbrirRespostaModeloFormulario(1);
            Assert.AreEqual(1, resposta.RespostaModeloFormularioID);
        }

        [TestMethod()]
        public void ResponderFormularioTest()
        {
            var result = svc.ResponderFormulario(1);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Respostas.Any());
            Assert.IsTrue(result.ModeloFormulario.Perguntas.Any());
            Assert.IsTrue(result.Respostas.Count == result.ModeloFormulario.Perguntas.Count);
        }

        [TestMethod()]
        public void GravarModeloFormularioTest()
        {
            var modelo = svc.AbrirFormulario(1);

            modelo.Perguntas.Last().Deleted = true;
            var pID = modelo.Perguntas.Last().PerguntaID;
            svc.GravarModeloFormulario(modelo, "ADMIN");
            svc.Commit("ADMIN");

            modelo = svc.AbrirFormulario(1);
            Assert.IsFalse(modelo.Perguntas.Any(d => d.PerguntaID == pID));
        }

        [TestMethod()]
        public void GravarRespostaModeloFormularioTest()
        {
            var respostaModelo = svc.AbrirRespostaModeloFormulario(1);
            var p = respostaModelo.ModeloFormulario.Perguntas.First(d => d.TipoPergunta == eTipoPergunta.Texto);
            var r = respostaModelo.Respostas.First(c => c.PerguntaID == p.PerguntaID);
            r.Valor = "My name is John";
            svc.GravarRespostaModeloFormulario(respostaModelo, "ADMIN");
            svc.Commit("ADMIN");

            respostaModelo = svc.AbrirRespostaModeloFormulario(1);
            r = respostaModelo.Respostas.First(c => c.PerguntaID == p.PerguntaID);

            Assert.AreEqual("My name is John", r.Valor.ToString());
        }
    }
}