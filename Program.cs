using System.Xml;

namespace AOC2025Day12
{
    public class Program
    {
        public static string PartOne(string data)
        {
            Dictionary<string, string[]> connections = new Dictionary<string, string[]>(); // connections[source] == [destinations]
            foreach (string line in data.Split(Environment.NewLine))
            {
                string source = line.Split(": ")[0];
                string destinationBits = line.Split(": ")[1];
                string[] destinations = destinationBits.Split(" ");
                connections[source] = destinations;
            }
            List<List<string>> paths = new List<List<string>>() { new List<string>() { "you" } };
            int finishedPaths = 0;
            while (paths.Count > 0)
            {
                List<List<string>> newPaths = new List<List<string>>();
                foreach (List<string> path in paths)
                {
                    string currentPlace = path.Last();
                    foreach (string nextPlace in connections[currentPlace])
                    {
                        if (nextPlace == "out")
                        {
                            finishedPaths++;
                        }
                        else
                        {
                            List<string> newPath = path.Append(nextPlace).ToList();
                            newPaths.Add(newPath);
                        }
                    }
                }
                paths = new List<List<string>>(newPaths);
            }
            return Convert.ToString(finishedPaths);
        }
        public static string PartTwo(string data)
        {
            Dictionary<string, string[]> connections = new Dictionary<string, string[]>(); // connections[source] == [destinations]
            foreach (string line in data.Split(Environment.NewLine))
            {
                string source = line.Split(": ")[0];
                string destinationBits = line.Split(": ")[1];
                string[] destinations = destinationBits.Split(" ");
                connections[source] = destinations;
            }
            List<List<string>> paths = new List<List<string>>() { new List<string>() { "svr" } };
            int finishedPaths = 0;
            while (paths.Count > 0)
            {
                List<List<string>> newPaths = new List<List<string>>();
                foreach (List<string> path in paths)
                {
                    string currentPlace = path.Last();
                    foreach (string nextPlace in connections[currentPlace])
                    {
                        if (nextPlace == "out")
                        {
                            if (path.Contains("fft") && path.Contains("dac"))
                            {
                                finishedPaths++;
                            }
                        }
                        else
                        {
                            List<string> newPath = path.Append(nextPlace).ToList();
                            newPaths.Add(newPath);
                        }
                    }
                }
                paths = new List<List<string>>(newPaths);
            }
            return Convert.ToString(finishedPaths);
        }
        static void Main()
        {
            string file = File.ReadAllText(@"../../../input.txt");
            Console.WriteLine(PartOne(file));
            Console.WriteLine(PartTwo(@"svr: aaa bbb
aaa: fft
fft: ccc
bbb: tty
tty: ccc
ccc: ddd eee
ddd: hub
hub: fff
eee: dac
dac: fff
fff: ggg hhh
ggg: out
hhh: out"));
            Console.WriteLine(PartTwo(file));
        }
    }
}
