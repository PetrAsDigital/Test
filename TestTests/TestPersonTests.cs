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
        [TestMethod()]
        public void TestDataTest()
        {
            var httpCommunication = new HttpComm();
            var response = httpCommunication.Execute("http://agl-developer-test.azurewebsites.net/people.json", null, "GET", "application/json", typeof(List<Person>));
            
            Assert.IsTrue(response is List<Person>);
            Assert.IsTrue(((List<Person>)response).Count.Equals(6));
            Assert.IsTrue(((List<Person>)response).Where(a => a.name == "Bob").Select(a => a.age).FirstOrDefault() == 23);
        }
    }
}