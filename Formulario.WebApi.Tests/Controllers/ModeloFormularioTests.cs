using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulario.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.Business;
using Formulario.Business.RepositoryPattern;
using Formulario.WebApi.Tests.MockData;
using Formulario.WebApi2.Controllers;
using System.Net.Http;
using System.Web.Http;
using Moq;
using System.Web.Http.Routing;
using Formulario.Business.DTO;

namespace Formulario.WebApi.Controllers.Tests
{
    [TestClass()]
    public class ModeloFormularioTests
    {
        IFormularioService svc;
        IDatabaseFactory factory;
        IUnitOfWOrkGeneric<IDatabaseFactory> unit;
        ModeloFormularioController controller;

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

            controller = new ModeloFormularioController(new Lazy<IFormularioService>(() => svc));
        }

        [TestMethod()]
        public void GetTest()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            string locationUrl = "http://location/";

            // Create the mock and set up the Link method, which is used to create the Location header.
            // The mock version returns a fixed string.
            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns(locationUrl);
            controller.Url = mockUrlHelper.Object;

            var result = controller.Get(1);

            result.Result
                .ExecuteAsync(System.Threading.CancellationToken.None)
                .Result
                .TryGetContentValue(out ModeloFormularioDTO val);

            Assert.AreEqual(1, val.ModeloFormularioID);
            Assert.AreEqual("Modelo", val.Descricao);
            Assert.AreEqual("<b>Modelo</b>", val.Html);
        }

        [TestMethod()]
        public void CreateTest()
        {
            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/modelodeformulario")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "modelodeformulario" } });

            var perguntaNumero = new PerguntaDTO
            {
                CasasDecimais = 2,
                Deleted = false,
                Descricao = "Desc",
                Obrigatorio = true,
                PatternRegex = @"\w+",
                PerguntaID = 0,
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
            };

            var modeloFormulario = new ModeloFormularioDTO
            {
                Descricao = "M",
                Html = "H",
                ModeloFormularioID = 0,
                Perguntas = new List<PerguntaDTO>
                    {
                        perguntaNumero
                    }
            };

            controller.Request.Headers.Add("ResultObject", "");

            // Act            
            var response = controller.Post(modeloFormulario)
                .Result
                .ExecuteAsync(System.Threading.CancellationToken.None)
                .Result
                .TryGetContentValue(out ModeloFormularioDTO val);

            Assert.AreEqual("M", val.Descricao);
            Assert.AreEqual("H", val.Html);
            Assert.AreEqual("Desc", val.Perguntas.First().Descricao);
            Assert.AreEqual("Titulo", val.Perguntas.First().Titulo);
            Assert.AreEqual("R$", val.Perguntas.First().Prefixo);
            Assert.AreEqual("%", val.Perguntas.First().Sufixo);
            Assert.IsNull(val.Perguntas.First().PatternRegex);
        }

        [TestMethod()]
        public void PutTest()
        {
            // Arrange

            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/modelodeformulario")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "modelodeformulario" } });

            var modeloFormulario = svc.AbrirFormulario(1);

            modeloFormulario.Perguntas.First().Titulo += "-Teste";

            controller.Request.Headers.Add("ResultObject", "true");
            // Act            
            var response = controller.Put(modeloFormulario)
                .Result
                .ExecuteAsync(System.Threading.CancellationToken.None)
                .Result
                .TryGetContentValue(out ModeloFormularioDTO val);

            var expected = modeloFormulario.Perguntas.First().Titulo;

            modeloFormulario = svc.AbrirFormulario(1);

            Assert.AreEqual(expected, modeloFormulario.Perguntas.First().Titulo);

            controller.Request.Headers.Clear();
            // Act            
            response = controller.Post(modeloFormulario)
               .Result
               .ExecuteAsync(System.Threading.CancellationToken.None)
               .Result
               .TryGetContentValue(out long id);

            Assert.AreEqual(1, id);
        }

        [TestMethod()]
        public void DeleteTestObject()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            controller.Request.Headers.Add("ResultObject", "");

            string locationUrl = "http://location/";

            // Create the mock and set up the Link method, which is used to create the Location header.
            // The mock version returns a fixed string.
            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns(locationUrl);
            controller.Url = mockUrlHelper.Object;

            var result = controller.Delete(1);

            result.Result
                .ExecuteAsync(System.Threading.CancellationToken.None)
                .Result
                .TryGetContentValue(out ModeloFormularioDTO val);

            Assert.AreEqual(1, val.ModeloFormularioID);
            Assert.AreEqual("Modelo", val.Descricao);
            Assert.AreEqual("<b>Modelo</b>", val.Html);
        }

        [TestMethod()]
        public void DeleteTestID()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            //controller.Request.Headers.Add("ResultObject", "");

            string locationUrl = "http://location/";

            // Create the mock and set up the Link method, which is used to create the Location header.
            // The mock version returns a fixed string.
            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns(locationUrl);
            controller.Url = mockUrlHelper.Object;

            var result = controller.Delete(1);

            result.Result
                .ExecuteAsync(System.Threading.CancellationToken.None)
                .Result
                .TryGetContentValue(out long val);

            Assert.AreEqual(1, val);

        }
    }
}