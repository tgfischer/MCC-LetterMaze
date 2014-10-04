using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter_Maze
{
    class Path
    {
        public LinkedList<Node> Items;

        public Path()
        {
            this.Items = new LinkedList<Node>();
        }

        public Node Remove(int x, int y)
        {
            foreach (Node node in this.Items)
            {
                if (x == node.X && y == node.Y)
                {
                    this.Items.Remove(node);
                    return node;
                }
            }

            return null;
        }
    }
}
