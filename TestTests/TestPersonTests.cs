using HttpCommunication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poco;
using System.Collections.Generic;
using System.Linq;

namespace Test.Tests.Tests
{
    [TestClass()]
    public class TestPersonTests
    {
        HttpComm httpCommunication;

        public TestPersonTests()
        {
            httpCommunication = new HttpComm();
        }

        [TestMethod()]
        public void TestDataConsistency()
        {
            var response = httpCommunication.Execute(UrlList.PetsUrl, null, "GET", "application/json", typeof(List<Person>));

            Assert.IsTrue(CheckTypeAndData(response));
        }

        [TestMethod()]
        public void TestCats()
        {
            var response = httpCommunication.Execute(UrlList.PetsUrl, null, "GET", "application/json", typeof(List<Person>));

            Assert.IsTrue(CheckTypeAndData(response));

            var typedResponse = (List<Person>)response;
            var onlyCats = new List<Pet>();
            foreach (var person in typedResponse.Where(a => a.pets != null))
            {
                foreach (var pet in person.pets.Where(a => a.type?.ToLower() == "cat"))
                {
                    onlyCats.Add(pet);
                }
            }


            DataManip dataManip = new DataManip();
            var resultFromDataManip = dataManip.GetAllCats(typedResponse, false);

            var firstNotSecond = onlyCats.Except(resultFromDataManip).ToList();
            var secondNotFirst = resultFromDataManip.Except(onlyCats).ToList();

            Assert.IsTrue(onlyCats.Count == resultFromDataManip.Count);
            Assert.IsFalse(!onlyCats.Any() && !secondNotFirst.Any());
        }

        private bool CheckTypeAndData(object response)
        {
            var result = response is List<Person>;
            result &= ((List<Person>)response).Any();

            return result;
        }
    }
}