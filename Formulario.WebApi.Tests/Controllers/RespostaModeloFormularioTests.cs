using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulario.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.Business;
using Formulario.Business.RepositoryPattern;
using Formulario.WebApi2.Controllers;
using System.Net.Http;
using System.Web.Http;
using Moq;
using System.Web.Http.Routing;
using Formulario.Business.DTO;
using Formulario.WebApi.Tests.MockData;

namespace Formulario.WebApi.Controllers.Tests
{
    [TestClass()]
    public class RespostaModeloFormularioTests
    {
        IFormularioService svc;
        IDatabaseFactory factory;
        IUnitOfWOrkGeneric<IDatabaseFactory> unit;

        RespostaModeloFormularioController controller;

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

            controller = new RespostaModeloFormularioController(new Lazy<IFormularioService>(() => svc));
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
                .TryGetContentValue(out RespostaModeloFormularioDTO val);

            Assert.AreEqual(1, val.RespostaModeloFormularioID);
            Assert.AreEqual("Modelo", val.ModeloFormulario.Descricao);
            Assert.AreEqual("<b>Modelo</b>", val.ModeloFormulario.Html);
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

            var respDTO = svc.ResponderFormulario(1);

            var pText = respDTO.Respostas.Single(c => c.PerguntaID == 2);
            var pIdade = respDTO.Respostas.Single(c => c.PerguntaID == 5);

            pText.Valor = "Hernandes";
            pIdade.Valor = 50;

            controller.Request.Headers.Add("ResultObject", "");

            var response = controller.Post(respDTO)
                .Result
            .ExecuteAsync(System.Threading.CancellationToken.None)
            .Result
            .TryGetContentValue(out RespostaModeloFormularioDTO val);

            Assert.AreEqual("Hernandes", val.Respostas.Single(c => c.PerguntaID == 2).Valor);
            Assert.AreEqual(50m, val.Respostas.Single(c => c.PerguntaID == 5).Valor);
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

            var respDTO = svc.AbrirRespostaModeloFormulario(1);

            var pTexto = respDTO.ModeloFormulario.Perguntas.Where(c => c.TipoPergunta == eTipoPergunta.Texto).First();
            respDTO.Respostas.Where(c => c.PerguntaID == pTexto.PerguntaID).First().Valor = "Teste-Teste";

            //var pAnexo = respDTO.ModeloFormulario.Perguntas.Where(c => c.TipoPergunta == eTipoPergunta.Anexo).First();
            //respDTO.Respostas.Where(c => c.PerguntaID == pAnexo.PerguntaID).First().Valor = new { AnexoID = 0, Nome = "", Extensao = "" };

            controller.Request.Headers.Add("ResultObject", "true");

            // Act            
            var response = controller.Put(respDTO)
                .Result
                .ExecuteAsync(System.Threading.CancellationToken.None)
                .Result
                .TryGetContentValue(out RespostaModeloFormularioDTO val);

            Assert.AreEqual("Teste-Teste", val.Respostas.Where(c => c.PerguntaID == pTexto.PerguntaID).First().Valor);

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
                .TryGetContentValue(out RespostaModeloFormularioDTO val);
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