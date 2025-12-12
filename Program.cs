namespace AOC2025Day11
{
    public class Program
    {
        public static int NumberOfRoutesBetween(string source, string destination, string intermediary, bool isAvoid, Dictionary<string, string[]> connections)
            // if isAvoid, avoids intermediary, otherwise requires it
        {
            List<List<string>> paths = new List<List<string>>() { new List<string>() { source } };
            int finishedPaths = 0;
            while (paths.Count > 0)
            {
                List<List<string>> newPaths = new List<List<string>>();
                foreach (List<string> path in paths)
                {
                    string currentPlace = path.Last();
                    foreach (string nextPlace in connections[currentPlace])
                    {
                        if (nextPlace == destination)
                        {
                            if (isAvoid)
                            {
                                if (intermediary != destination & !path.Contains(intermediary)) // Destination is never added to path, so need to check
                                {
                                    finishedPaths++;
                                }
                            }
                            else
                            {
                                if (intermediary == destination || path.Contains(intermediary)) // Destination is never added to path, so need to check
                                {
                                    finishedPaths++;
                                }
                            }
                        }
                        else
                        {
                            List<string> newPath = path.Append(nextPlace).ToList();
                            newPaths.Add(newPath); // Too memory intensive for laptop
                        }
                    }
                }
                paths = new List<List<string>>(newPaths);
            }
            return finishedPaths;
        }
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
            return Convert.ToString(NumberOfRoutesBetween("you", "out", "you", false, connections));
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
            //List<List<string>> paths = new List<List<string>>() { new List<string>() { "svr" } };
            //int finishedPaths = 0;
            //while (paths.Count > 0)
            //{
            //    List<List<string>> newPaths = new List<List<string>>();
            //    foreach (List<string> path in paths)
            //    {
            //        string currentPlace = path.Last();
            //        foreach (string nextPlace in connections[currentPlace])
            //        {
            //            if (nextPlace == "out")
            //            {
            //                if (path.Contains("fft") && path.Contains("dac"))
            //                {
            //                    finishedPaths++;
            //                }
            //            }
            //            else
            //            {
            //                List<string> newPath = path.Append(nextPlace).ToList();
            //                newPaths.Add(newPath); // Too memory intensive for laptop
            //            }
            //        }
            //    }
            //    paths = new List<List<string>>(newPaths);
            //}
            //return Convert.ToString(finishedPaths);
            // The number of paths from svr to out via fft and dac =
            //    (number from svr to dac via fft) * (number from dac to out)
            //  + (number from svr to dac not via fft) * (number from dac to out via fft)
            int svr_to_dac_via_fft = NumberOfRoutesBetween("svr", "dac", "fft", false, connections);
            int dac_to_out = NumberOfRoutesBetween("dac", "out", "dac", false, connections);
            int svr_to_dac_not_via_fft = NumberOfRoutesBetween("svr", "dac", "fft", true, connections);
            int dac_to_out_via_fft = NumberOfRoutesBetween("dac", "out", "fft", false, connections);
            return Convert.ToString(svr_to_dac_via_fft * dac_to_out + svr_to_dac_not_via_fft * dac_to_out_via_fft);
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
