using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestWay
{
    public class World
    {
        public Cell[,] сells; // World map

        public World(int sizeX, int sizeY)
        {
            var rnd = new Random();

            // Build map and randomly set passability for each cell
            сells = new Cell[sizeX, sizeY];
            for (int x = 0; x < sizeX; x++)
                for (int y = 0; y < sizeY; y++)
                    сells[x, y] = new Cell(x, y, (byte)rnd.Next(0, 100));
        }

        public Location[] FindShortestWay(Location start, Location end)
        {
            // TODO: Implement finding most shortest way between start and end locations
            
            if (сells[start.X, start.Y].Passability == 0 || сells[end.X, end.Y].Passability == 0) return new Location[0];

            Dictionary<Location, Location> prev = new Dictionary<Location, Location>();
            Dictionary<Location, double> cost = new Dictionary<Location, double>();
            PriorityQueue<Location> locs = new PriorityQueue<Location>();
            locs.Enqueue(start, 0);
            prev[start] = start;
            cost[start] = 0;

            while (locs.Count > 0) {
                var curr = locs.Dequeue();

                if (curr.Equals(end) || (curr.X == end.X && curr.Y == end.Y)) {
                    break;
                }

                foreach (var next in GetNeighbours(curr)) {
                    double newCost = cost[curr] + (100 - сells[next.X, next.Y].Passability);  // higher passability = lower way cost
                    double priority = newCost + GetHeuristic(next, end);
                    if (!cost.ContainsKey(next) || newCost < cost[next]) {
                        cost[next] = newCost;
                        locs.Enqueue(next, priority);
                        prev[next] = curr;
                    }
                }
            }

            //try to collect route
            Stack<Location> result = new Stack<Location>();
            var tail = prev.Where(t => t.Key.X == end.X && t.Key.Y == end.Y).FirstOrDefault().Key;
            if (tail == null) return new Location[0]; // if route not found

            result.Push(tail);
            while (tail != start){
                tail = prev[tail];
                result.Push(tail);                
            }

            return result.ToArray();
        }

        private Location[] GetNeighbours(Location loc) {
            List<Location> result = new List<Location>();
            if (loc.X > 0) result.Add(сells[loc.X -1, loc.Y]);
            if(loc.X < сells.GetUpperBound(0)) result.Add(сells[loc.X + 1, loc.Y]);
            if (loc.Y > 0) result.Add(сells[loc.X, loc.Y - 1]);
            if (loc.Y < (сells.GetValue(сells.GetUpperBound(сells.Rank - 2), сells.GetUpperBound(сells.Rank - 1)) as Location).Y) result.Add(сells[loc.X, loc.Y + 1]);

            return result.Where(l => сells[l.X, l.Y].Passability > 0).ToArray();
        }

        private double GetHeuristic(Location startLoc, Location endLoc)
        {
            return Math.Abs(startLoc.X - endLoc.X) + Math.Abs(startLoc.Y - endLoc.Y);
        }
    }
}
