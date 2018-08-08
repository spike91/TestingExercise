using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestWay
{
    public class PriorityQueue<T>
    {
        private List<Tuple<T, double>> elements = new List<Tuple<T, double>>();

        public int Count
        {
            get { return elements.Count; }
        }

        public void Enqueue(T item, double priority)
        {
            elements.Add(Tuple.Create(item, priority));
        }

        public T Dequeue()
        {
            double bestPriority = elements.OrderBy(t => t.Item2).FirstOrDefault().Item2;
            T bestItem = elements.FirstOrDefault().Item1;
            foreach (var item in elements)
            {
                if (item.Item2.Equals(bestPriority))
                {
                    bestItem = item.Item1;
                }
            }
            elements.Remove(elements.Where(t => t.Item1.Equals(bestItem)).FirstOrDefault());
            return bestItem;
        }
    }
}
