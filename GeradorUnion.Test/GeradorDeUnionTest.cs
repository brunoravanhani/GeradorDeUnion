using GeradorUnion.Test.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GeradorUnion.Test
{
    [TestClass]
    public class GeradorDeUnionTest
    {
        [TestMethod]
        [DataRow(3, "select")]
        [DataRow(2, "union")]
        [DataRow(3, "Tabela")]
        public void ShouldValidateGerador(int numberOcurrences, string word)
        {
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Nome = "Teste1", Idade = 18 },
                new Pessoa { Nome = "Teste1", Idade = 18 },
                new Pessoa { Nome = "Teste1", Idade = 18 }
            };

            var gerador = new GeradorDeUnion<Pessoa>(pessoas);

            var result = gerador.Gerar();

            var regex = new Regex(word, RegexOptions.IgnoreCase);

            Assert.AreEqual(numberOcurrences, regex.Matches(result).Count);
        }


        
    }
}
