Microsoft Coding Challenge - Letter Maze
==============

This was one of the problems that was my team was asked during the Microsoft Coding Challenge at Western University in September. Unfortunately, my team did not finish this problem within the time frame. However, I decided to complete the problem at home on my own time anyways. Here is what was given to us:

> You are faced with a stepping puzzle. You know the word needed to get from one side to the other safely, but you need to find the path.

> Using a given word and 2D grid of letters, find a path from the top left to the bottom right of the grid. This path must only use adjacent spaces (no diagonals) and it can't use the same space twice. After you have found the path, change all other letters in the grid to periods (`.`) to let the rest of your friends cross safely.

Input description/format
--------------
The input will be the word to follow followed by a grid of characters.

Output description/format
--------------
The output will be the grid of characters with only the correct path remaining, each other letter in the grid will be replaced with a period (`.`)

Example input
--------------
```
SNICKERDOODLE
SNICKE
NRCRDO
IEKODS
CRDOLE
```

Example output
--------------
```
SNI...
..C...
.EKOD.
.RDOLE
```

My Solution
==============
I decided to use a breadth first search to complete this problem. There is a grid of characters, which is initialized like so (Each `char[]` is a row, while each `List` item is a column)

```cs
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
```

Next, I dequeue an item in the `activeFront`, and check its neighbouring characters. If a neighbouring character matches the one I am looking for, I create a new `Node` and add it to the `activeFront`. Each `Node` holds a reference to the previous `Node`. The first node is the upper left hand corner of the grid, and this loops until I reach the bottom right hand corner of the grid

```cs
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
```

Once I find the last node, I need to draw the path. I first create a `Path` object, and store each item in the path to the underlying `LinkedList<Node>`. I then go through each charcter in the grid, and if the coordinates match the coordinates of a node in the `LinkedList<Node>`, then I output that Node's character. Otherwise I output a `.`

```cs
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
```

Example
--------------

Here is an example from `2.3.txt`, the largest input file I was provided with

### Input
```
MNGFBQVJSGUEPOWVRXMIYUOPQLMUJARPPSLBHOOCFPTAXJVFOCWOQWGRZQPQDBZSPEPWCVOCIKDJAOSHPHFGDCYSLUCRZYYDPBKSTYDTOUFGTXQLXHAWVTOMGWLISXDK
MNGNAARNAIBFZAAHSKOTSIQZVRICDGZFTVJWTTOOXPAJKY
NAFFXIFGAPMSKWLAHQBRSPRVNJNFNNGQDJDAYQDOXBFAJC
XQBETGOYUIOKWERNUATNIWOBSLBRNNURKQMLMILGZHVVUT
DVUYPTXHRRNNWLIFWKHOXAFSGBMPNOAATGZHHTXTXJSZYX
SJPXCBCWOGWSYBROHIUDQMTPLBYDNNRNIGTJLOMMLEMMKG
GUEPOJFBJMOPPIMDZXJVFOCLACZJBPHFGISLKRHICSVIRM
AGFEWSSBDBCYTXHFMADMJMWOQULXBWPRDCYUCYSHTTGXVM
WUGIVRKXGZNKRTAFPTDQPJZGWZSPEPHSOAESRYDPJEIATJ
WHIRVXCVISWDYPNCLJRFXYPRDBQDKWIKDJHTZYPDXNPYHQ
QXIRPMBGSQLVSUOOOEVSCRWZQQQFHCCBEYWEYTBJQDPCVO
ICVQKIYUOPMPPBHCLULGKIMQPUUJDVOBAGJIDSKVDSSXWB
RCSSQNQHALURSLNEKVUUQJJPHAOBPFZGRYRFTOOSTESPVH
EBCPNBYNBJJACINHUROQCLNJQCTHQSCQBAOCFUKFXYOOGM
FHWJSGYCPBMKTVLAVFKGKKIBULXOTPCOEBEIGTXQGKPJTA
BZNEIPUXCIDABVZUOHJKIZZYFSRDTLQAMKIINQCLITWNTK
EZIJLVIAJRBAPYPYRERSCNQMQHYEAQLMGNBSITHXHAPHAD
SHIYRMKCSWAIQIWTEXEZSFPTBXXQXWBKCKXFHIGRVWQNTQ
FFKJLACSDAKMRPWUPTEVUPFFKRLWQNIEMZOWMNEXTTIHBZ
LNSMMBWJXVQZEYUOGBQRFARBAHCPHCLIDGZDTJDLOLVOEY
WHOKINGQJUKWRLFYHBVJHWYZQVWXEGJLNDEZNEZOMGWFRT
MPHCXYMEUTVQAJPUNQILDKZJOHGLCCGEPCMKJSYZRBLISP
SRMEQYKLYWGPBQYOQSBBSMRAXWFKURJTCVGRBDPMSQIIXD
JMKTLEIGQAWGALGUTZQGMMNGJGMMCPJBDIRLLLZQJCDBYK
```

### Output
```
MNG...........................................
..F...........................................
.QB...........................................
.V............................................
SJ............................................
GUEPO............XJVFOC.......HFG.SL..........
....W............A....WOQ.....P.DCYUC.........
....VR.........FPT.....GWZSPEPHSOA..RYD.......
.....X.........C.......RDB...WIKDJ..ZYP.......
.....M...QL...OO.......ZQ....CC.....YTB.......
.....IYUOPMPPBH........QP....VO.....DSK.......
..........URSL......................TO........
..........JA........................FU........
....................................GTXQ......
.......................................L......
.......................................XHA....
........................................VW....
........................................T.....
........................................O.....
........................................MGW...
..........................................LIS.
............................................XD
.............................................K
```

