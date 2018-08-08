using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestWay
{
    public class Cell : Location
    {
        // Defines cell passability from 0 (can't go) to 100 (normal passability)
        // The higher is passability, the quicker it is possible to pass the cell
        public byte Passability;

        public Cell(int x, int y, byte passability)
            : base(x, y)
        {
            this.Passability = passability;
        }

        public override bool Equals(object obj)
        {
            var cell = obj as Cell;
            return cell != null &&
                   base.Equals(obj);
        }
    }
}
