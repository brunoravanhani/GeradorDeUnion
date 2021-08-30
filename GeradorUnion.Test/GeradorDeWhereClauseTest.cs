using GeradorUnion.Test.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeradorUnion.Test
{
    [TestClass]
    public class GeradorDeWhereClauseTest
    {
        [TestMethod("Should validate GetWhereClause with all properties")]
        public void ShouldValidadeWhereAllProperties()
        {

            var pessoa = new Pessoa { Nome = "Teste", Idade = 17, Sobrenome = "Da Silva" };

            var gerador = new GeradorDeWhereClause<Pessoa>(pessoa);

            var result = gerador.GetWhereClause();

            Assert.AreEqual(" WHERE Nome = 'Teste' AND Sobrenome = 'Da Silva' AND Idade = 17", result);
        }

        [TestMethod("Should validate GetWhereClause with null object")]
        public void ShouldValidadeWhereNull()
        {

            var gerador = new GeradorDeWhereClause<Pessoa>(null);

            var result = gerador.GetWhereClause();

            Assert.AreEqual("", result);
        }

        [TestMethod("Should validate GetWhereClause with property null")]
        public void ShouldValidadeWherePropertyNull()
        {

            var pessoa = new Pessoa { Nome = "Teste", Idade = 17 };

            var gerador = new GeradorDeWhereClause<Pessoa>(pessoa);

            var result = gerador.GetWhereClause();

            Assert.AreEqual(" WHERE Nome = 'Teste' AND Idade = 17", result);
        }

        [TestMethod("Should validate GetWhereClause with empty object")]
        public void ShouldValidadeWhereEmpty()
        {

            var pessoa = new Pessoa();

            var gerador = new GeradorDeWhereClause<Pessoa>(pessoa);

            var result = gerador.GetWhereClause();

            Assert.AreEqual("", result);
        }
    }
}
