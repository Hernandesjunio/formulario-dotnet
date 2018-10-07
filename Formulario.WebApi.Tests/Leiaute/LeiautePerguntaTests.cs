using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulario.Business.Leiaute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.Business.Perguntas;

namespace Formulario.Business.Leiaute.Tests
{
    [TestClass()]
    public class LeiautePerguntaTests
    {
        [TestMethod()]
        public void LeiautePadraoTest()
        {
            LeiautePergunta leiaute = LeiautePergunta.LeiautePadrao(new PerguntaTexto());

            Assert.AreEqual(eColunas.T12, leiaute.LeiauteItem.First().Tamanho);
            Assert.AreEqual(eTamanhoTela.Mobile, leiaute.LeiauteItem.First().Responsivo);
        }
    }
}