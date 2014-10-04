MCC-LetterMaze
==============

This was one of the problems that was my team was asked during the Microsoft Coding Challenge at Western University in September. Unfortunately, my team did not finish this problem within the time frame. However, I decided to complete the problem at home on my own time anyways. Here is what was given to us:

> You are faced with a stepping puzzle. You know the word needed to get from one side to the other safely, but you need to find the path.

> Using a given word and 2D grid of letters, find a path from the top left to the bottom right of the grid. This path must only use adjacent spaces (no diagonals) and it can't use the same space twice. After you have found the path, change all other letters in the grid to periods (`.`) to let the rest of your friends cross safely.

Input description/format
==============
The input will be the word to follow followed by a grid of characters.

Output description/format
==============
The output will be the grid of characters with only the correct path remaining, each other letter in the grid will be replaced with a period (`.`)

Example input
==============
```
SNICKERDOODLE
SNICKE
NRCRDO
IEKODS
CRDOLE
```

Example output
==============
```
SNI...
..C...
.EKOD.
.RDOLE
```
