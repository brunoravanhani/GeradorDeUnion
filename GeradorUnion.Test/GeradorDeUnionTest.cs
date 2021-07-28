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

            var gerador = new GeradorDeUnion<Pessoa>();

            var result = gerador.Gerar(pessoas);

            var regex = new Regex(word, RegexOptions.IgnoreCase);

            Assert.AreEqual(numberOcurrences, regex.Matches(result).Count);
        }


        [TestMethod("Should validate GetWhereClause with all properties")]
        public void ShouldValidadeWhereAllProperties()
        {
            var gerador = new GeradorDeUnion<Pessoa>();

            var pessoa = new Pessoa { Nome = "Teste", Idade = 17, Sobrenome = "Da Silva" };

            var result = gerador.GetWhereClause(pessoa);

            Assert.AreEqual(" WHERE Nome = 'Teste' AND Sobrenome = 'Da Silva' AND Idade = 17", result);
        }

        [TestMethod("Should validate GetWhereClause with null object")]
        public void ShouldValidadeWhereNull()
        {
            var gerador = new GeradorDeUnion<Pessoa>();

            var pessoa = new Pessoa { Nome = "Teste", Idade = 17, Sobrenome = "Da Silva" };

            var result = gerador.GetWhereClause(null);

            Assert.AreEqual("", result);
        }

        [TestMethod("Should validate GetWhereClause with property null")]
        public void ShouldValidadeWherePropertyNull()
        {
            var gerador = new GeradorDeUnion<Pessoa>();

            var pessoa = new Pessoa { Nome = "Teste", Idade = 17 };

            var result = gerador.GetWhereClause(pessoa);

            Assert.AreEqual(" WHERE Nome = 'Teste' AND Idade = 17", result);
        }


        class Pessoa
        {
            public string Nome { get; set; }
            public string Sobrenome { get; set; }
            public int Idade { get; set; }
        }
    }
}
