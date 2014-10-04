using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Letter_Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char[]> grid = new List<char[]>();
            String phrase = "";

            using (StreamReader reader = new StreamReader("2.3.txt")) 
            {
                phrase = reader.ReadLine();

                for (int i = 0; !reader.EndOfStream; i++)
                {
                    String line = reader.ReadLine();
                    grid.Add(new char[line.Length]);

                    for (int j = 0; j < line.Length; j++)
                    {
                        grid.ElementAt(i)[j] = line.ElementAt(j);
                    }
                }
            }

            BreadthFirstSearch(grid, phrase);

            Console.ReadLine();
        }

        static void BreadthFirstSearch(List<char[]> grid, String phrase)
        {
            Point[] DIRECTION = new Point[] { new Point(0, 1), new Point(1, 0), new Point(0, -1), new Point(-1, 0) };
            Queue<Node> activeFront = new Queue<Node>();
            Node start = new Node(0, 0, 0, grid.ElementAt(0)[0]), end = null;
            
            start.NextLetter = phrase.ElementAt(1);
            activeFront.Enqueue(start);

            while (activeFront.Count != 0 && end == null) 
            {
                Node node = activeFront.Dequeue();

                for (int j = 0; j < 4; j++)
                {
                    if (node.X + DIRECTION[j].X < 0 || node.X + DIRECTION[j].X > grid.ElementAt(0).Length - 1 ||
                        node.Y + DIRECTION[j].Y < 0 || node.Y + DIRECTION[j].Y > grid.Count - 1)
                        continue;

                    char neighbour = grid.ElementAt(node.Y + DIRECTION[j].Y)[node.X + DIRECTION[j].X];

                    if (neighbour == node.NextLetter)
                    {
                        Node next = new Node(node.X + DIRECTION[j].X, node.Y + DIRECTION[j].Y, node.Index + 1, neighbour);
                        next.PreviousLetter = node;

                        if (next.X == grid.ElementAt(0).Length - 1 && next.Y == grid.Count - 1)
                        {
                            end = next;
                            break;
                        }

                        next.NextLetter = phrase.ElementAt(next.Index + 1);
                        activeFront.Enqueue(next);
                    }
                }
            }

            if (end != null) DrawPath(start, end, grid);
        }

        private static void DrawPath(Node start, Node end, List<char[]> grid)
        {
            Path path = new Path();
            path.Items.AddLast(end);

            while (path.Items.Last() != start)
            {
                path.Items.AddLast(path.Items.Last().PreviousLetter);
            }

            path.Items.AddLast(start);


            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid.ElementAt(0).Length; j++)
                {
                    Node node = path.Contains(j, i);

                    if (node == null) Console.Write('.');
                    else Console.Write(node.Letter);
                }

                Console.Write("\n");
            }
        }
    }
}
