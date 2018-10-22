using Formulario.Business.Perguntas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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

            foreach (Pergunta item in modelo.Perguntas)
            {
                Assert.AreEqual(eColunas.T12, item.LeiautePerguntas.Single().LeiauteItem.First().Tamanho);
                Assert.AreEqual(eTamanhoTela.Mobile, item.LeiautePerguntas.Single().LeiauteItem.First().Responsivo);
            }            
        }
    }
}