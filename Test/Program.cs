using HttpCommunication;
using Poco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpCommunication = new HttpComm();
            var response = httpCommunication.Execute("http://agl-developer-test.azurewebsites.net/people.json", null, "GET", "application/json", typeof(List<Person>));


            if (!(response is List<Person>))
                Console.WriteLine($"Response is not of desired type!");

            var typedResponse = ((List<Person>)response).ToList();

            DataManip dataManip = new DataManip();
            dataManip.GetAllCats(typedResponse, true);

            Console.WriteLine("press any key...");
            Console.ReadKey();
        }
    }
}
