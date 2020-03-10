using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace csharp6_features
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/exploration/csharp-6?tutorial-step=1
            var p = new Person("Bill", "Wagner");
            WriteLine($"The name, in all caps: {p.AllCaps()}");
            WriteLine($"The name is: {p}");


            //https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/exploration/csharp-6?tutorial-step=5
            var phrase = "the quick brown fox jumps over the lazy dog";
            var wordLength = from word in phrase.Split(' ') select word.Length;
            WriteLine($"The average word length is: {wordLength.Average()}");
            WriteLine($"The average word length is: {wordLength.Average():F2}");


            // https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/exploration/csharp-6?tutorial-step=6
            string s = null;
            bool hasMore = s?.ToCharArray()?.GetEnumerator()?.MoveNext() ?? false;
            WriteLine(hasMore);


            //https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/exploration/csharp-6?tutorial-step=7
            try
            {
                string s1 = null;
                WriteLine(s1.Length);

            }
            catch (Exception e) when (LogException(e))
            {
            }
            WriteLine("Exception must have been handled");


            // https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/exploration/csharp-6?tutorial-step=8
            WriteLine(nameof(System.String));
            int j = 5;
            WriteLine(nameof(j));
            List<string> names = new List<string>();
            WriteLine(nameof(names));

            WriteLine(nameof(p));
            WriteLine(nameof(p.FirstName));
            WriteLine(nameof(p.AllCaps));


            // https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/exploration/csharp-6?tutorial-step=9
            var messages = new Dictionary<int, string>
            {
                [404] = "Page not Found",
                [302] = "Page moved, but left a forwarding address.",
                [500] = "The web server can't come out to play today."
            };
            WriteLine(messages[302]);

            var capitals = new Dictionary<string, string>
            {
                ["india"] = "New Delhi",
                ["china"] = "Beijing",
                ["germany"] = "Berlin"
            };

            //WriteLine($"india1234 ---- {capitals["india1234"]}");
            WriteLine($"germany ---- {capitals["Germany".ToLower()]}");
        }



        private static bool LogException(Exception e)
        {
            WriteLine($"\tIn the log routine. Caught {e.GetType()}");
            WriteLine($"\tMessage: {e.Message}");
            return true; // <----- return false to not handle exception
        }
    }


    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Person(string first, string last)
        {
            FirstName = first;
            LastName = last;
        }

        public override string ToString() => $"{FirstName} {LastName}";

        public string AllCaps() => ToString().ToUpper();
    }
}
