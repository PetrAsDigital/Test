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
            var result = typedResponse.Where(a => a.pets != null).GroupBy(a => a.gender, a => a, (key, g) => new { Gender = key, PersonsInGroup = g });

            foreach (var group in result)
            {
                Console.WriteLine($"gender: {group.Gender}");

                foreach (var item in group.PersonsInGroup.SelectMany(a => a.pets.Where(b => b.type?.ToLower() == "cat").Select(b => b.name)).OrderBy(a => a).ToList())
                {
                    Console.WriteLine($" cat name: {item}");
                }

                Console.WriteLine("");
            }


            Console.WriteLine("press any key...");
            Console.ReadKey();
        }
    }
}
