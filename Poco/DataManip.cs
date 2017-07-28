using System;
using System.Collections.Generic;
using System.Linq;

namespace Poco
{
    public class DataManip
    {
        public List<Pet> GetAllCats(List<Person> typedResponse, bool printOutput)
        {
            var groupedResponse = typedResponse.Where(a => a.pets != null).GroupBy(a => a.gender, a => a, (key, g) => new { Gender = key, PersonsInGroup = g });

            var result = new List<Pet>();
            foreach (var group in groupedResponse)
            {
                if (printOutput)
                    Console.WriteLine($"gender: {group.Gender}");

                foreach (var item in group.PersonsInGroup.SelectMany(a => a.pets.Where(b => b.type?.ToLower() == "cat")).OrderBy(a => a.name).ToList())
                {
                    result.Add(item);

                    if (printOutput)
                        Console.WriteLine($" cat name: {item.name}");
                }

                if (printOutput)
                    Console.WriteLine("");
            }

            return result;
        }
    }
}
