using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter_Maze
{
    class Node : Point
    {
        public char Letter;
        public char NextLetter;
        public Node PreviousLetter;
        public int Index;

        public Node(int x, int y, int index, char letter) : base(x, y)
        {
            this.Letter = letter;
            this.Index = index;
        }
    }
}
