using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondChessEngine
{
    public struct MoveList
    {
        public MoveList()
        {
        }

        public ushort[] Moves { get; set; } = new ushort[256];
        public int Count { get; set; } = 0;
    }
}
