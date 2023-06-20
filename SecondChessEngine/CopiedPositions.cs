using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondChessEngine
{
    public static class CopiedPositions
    {
        public static Stack<Position> Positions { get; set; } = new Stack<Position>();
    }
}
