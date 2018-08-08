using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestWay
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }

        private static void Test() {
            World world = new World(10, 10);
            var rnd = new Random();
            //Location start = new Location(rnd.Next(10), rnd.Next(10));
            //Location end = new Location(rnd.Next(10), rnd.Next(10));
            Location start = new Location(0, 0);
            Location end = new Location(9, 9);
            Console.WindowHeight = 50;
            Console.WindowWidth = 100;
            Console.WriteLine(string.Format("Start location: ({0},{1})", start.X, start.Y));
            Console.WriteLine(string.Format("End location: ({0},{1})", end.X, end.Y));

            Location[] locations = world.FindShortestWay(start, end);
            if(locations.Count() == 0) Console.WriteLine("Route was not found.");
            int cost = 0;
            Console.WriteLine("---------------------------------------------------------------");
            int i = 0;
            foreach (var item in world.сells)
            {
                if (locations.Contains(item))
                {
                    cost += item.Passability;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(GetStr(string.Format("[{0}]", item.Passability)));
                    Console.ResetColor();
                }
                else Console.Write(GetStr(string.Format("{0} ", item.Passability)));
                i++;
                if (i % 10 == 0) Console.WriteLine("");
            }
            Console.WriteLine("---------------------------------------------------------------");
            Console.ReadLine();
        }

        private static string GetStr(string str) {
            string result = str;
            for (int i = 4; i > str.Length; i--){
                result = " " + result;
            }
            return result;
        }
    }
}
