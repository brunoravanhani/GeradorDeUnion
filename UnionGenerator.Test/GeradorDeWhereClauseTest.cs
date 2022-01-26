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

            var person = new Person { FirstName = "Teste", Age = 17, LastName = "Da Silva" };

            var generator = new WhereClauseGenerator<Person>(person);

            var result = generator.GetWhereClause();

            Assert.AreEqual(" WHERE FirstName = 'Teste' AND LastName = 'Da Silva' AND Age = 17", result);
        }

        [TestMethod("Should validate GetWhereClause with null object")]
        public void ShouldValidadeWhereNull()
        {

            var generator = new WhereClauseGenerator<Person>(null);

            var result = generator.GetWhereClause();

            Assert.AreEqual("", result);
        }

        [TestMethod("Should validate GetWhereClause with property null")]
        public void ShouldValidadeWherePropertyNull()
        {

            var person = new Person { FirstName = "Teste", Age = 17 };

            var generator = new WhereClauseGenerator<Person>(person);

            var result = generator.GetWhereClause();

            Assert.AreEqual(" WHERE FirstName = 'Teste' AND Age = 17", result);
        }

        [TestMethod("Should validate GetWhereClause with empty object")]
        public void ShouldValidadeWhereEmpty()
        {

            var person = new Person();

            var generator = new WhereClauseGenerator<Person>(person);

            var result = generator.GetWhereClause();

            Assert.AreEqual("", result);
        }
    }
}
