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
        [DataRow(3, "Table")]
        public void ShouldValidateGerador(int numberOcurrences, string word)
        {
            var people = new List<Person>
            {
                new Person { FirstName = "Teste1", Age = 18 },
                new Person { FirstName = "Teste1", Age = 18 },
                new Person { FirstName = "Teste1", Age = 18 }
            };

            var generator = new UnionGenerator<Person>(people);

            var result = generator.Generate();

            var regex = new Regex(word, RegexOptions.IgnoreCase);

            Assert.AreEqual(numberOcurrences, regex.Matches(result).Count);
        }


        
    }
}
