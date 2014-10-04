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

        public Node(int x, int y, int index, char letter) 
            : base(x, y)
        {
            this.Letter = letter;
            this.Index = index;
        }

        public Node(int x, int y, int index, char letter, Node previousNode)
            : this(x, y, index, letter)
        {
            this.PreviousLetter = previousNode;
        }

        public Node(int x, int y, int index, char letter, char nextLetter)
            : this(x, y, index, letter)
        {
            this.NextLetter = nextLetter;
        }
    }
}
