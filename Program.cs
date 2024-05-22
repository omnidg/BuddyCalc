using Buddies.App_Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Buddies
{
    internal class Program
    {
        static void Main(string[] args)
        {
            People people = new People();
            string URL = "https://swapi.dev/api/people";
            List<Result> listresutls = new List<Result>();
           
            string oldNext = "fe";

            string Result = "";
            Console.WriteLine("Loading Buddy List.....");
            while ((URL != null && URL.Length > 0) && oldNext != URL)
            {
                try
                {
                    Result = HelperClass.GetData(URL);
                    if (Result != null && Result.Length > 0)
                    {
                        people = new People();
                        people = JsonConvert.DeserializeObject<People>(Result);
                        people.results.ForEach(f => listresutls.Add(f));
                        oldNext = URL;
                        URL = people.next ;
                    }
                }
                catch { }
            }
            Console.WriteLine("Buddy List Loaded SUccessfully. Number of items: " + listresutls.Count);
            Console.WriteLine("Starting Buddy Compare. Please wait....");
            List<MatchingBuddies> matching = new List<MatchingBuddies>();

            List<string> joinedstrings = listresutls.Select(s => string.Join(",", s.films.OrderBy(o => o))).Distinct().ToList();
            joinedstrings.ForEach(f => matching.Add(new MatchingBuddies { Buddies = "", frims = f }));
            foreach (Result p in listresutls) 
            {
                MatchingBuddies m = matching.Where(w => w.frims == string.Join(",", p.films.OrderBy(o => o))).FirstOrDefault();
                if (m != null) 
                {
                    if(m.Buddies.Length > 0)
                    {
                        m.Buddies += "," + p.name;
                    }
                    else
                    {
                        m.Buddies += p.name;
                    }

                }
            }

            Console.WriteLine("Completed. Please see below output: ");

            if (matching.Count > 0)
            {
                matching.ForEach(o => Console.WriteLine("Matching Buddies: " + o.Buddies));
            }
            else
            {
                Console.WriteLine("No matching data found");
            }

            Console.ReadLine();
        }
    }
}
