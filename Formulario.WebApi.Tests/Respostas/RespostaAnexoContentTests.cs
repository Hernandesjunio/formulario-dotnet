using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulario.Business.Respostas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Business.Respostas.Tests
{
    [TestClass()]
    public class RespostaAnexoContentTests
    {
        [TestMethod()]
        public void ComputeChecksumTest()
        {
            //Arrange
            var r = new RespostaAnexoContent();
            r.Conteudo = new byte[10];

            //Act
            r.ComputeChecksum();

            //Assert
            Assert.AreEqual(r.Checksum, r.Conteudo.Checksum());
        }
    }
}