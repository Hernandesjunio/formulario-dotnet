using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulario.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.Business.Perguntas;

namespace Formulario.Business.Tests
{
    [TestClass()]
    public class ModeloDeFormularioTests
    {
        [TestMethod()]
        public void AtribuirLeiautePerguntasPadraoTest()
        {
            ModeloDeFormulario modelo = new ModeloDeFormulario();
            modelo.Perguntas.Add(new PerguntaTexto());
            modelo.Perguntas.Add(new PerguntaAnexo());
            modelo.Perguntas.Add(new PerguntaNumero());
            modelo.Perguntas.Add(new PerguntaData());
            modelo.Perguntas.Add(new PerguntaEscolhaUnica());
            modelo.Perguntas.Add(new PerguntaMultiplaEscolha());
            modelo.Perguntas.Add(new PerguntaGradeDeOpcoes());

            modelo.AtribuirLeiautePerguntasPadrao();

            foreach (var item in modelo.Perguntas)
            {
                Assert.AreEqual(eColunas.T12, item.LeiautePerguntas.Single().LeiauteItem.First().Tamanho);
                Assert.AreEqual(eTamanhoTela.Mobile, item.LeiautePerguntas.Single().LeiauteItem.First().Responsivo);
            }
        }
    }
}