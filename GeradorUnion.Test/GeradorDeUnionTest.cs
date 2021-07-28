using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeradorUnion.Test
{
    [TestClass]
    public class GeradorDeUnionTest
    {
        [TestMethod]
        public void ShouldValidateGerador()
        {
            var caracteristicas = new string[]
            {
                "teste",
                "teste2",
                "teste3"
            };

            var gerador = new GeradorDeUnion();

            var result = gerador.Gerar(caracteristicas);

            var expected = @"Select * from Tabela where caracteristica = 'teste' UNION Select * from Tabela where caracteristica = 'teste2' UNION Select * from Tabela where caracteristica = 'teste3'";

            Assert.AreEqual(
                expected
                , result);
        }
    }
}
