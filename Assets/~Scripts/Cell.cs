using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Cell
{
    public int x = 0;
    public int y = 0;

    public bool[] walls = new[] { true, true, true, true };
    public bool visited = false;

    public Cell PickNeighbour(Cell[,] board)
    {
        List<Cell> neighbours = new List<Cell>();
        if (this.y > 0)
        {
            neighbours.Add(board[this.x, this.y - 1]);
        }
        if (this.x < board.GetLength(0) - 1)
        {
            neighbours.Add(board[this.x + 1, this.y]);
        }
        if (this.y < board.GetLength(1) - 1)
        {
            neighbours.Add(board[this.x, this.y + 1]);
        }
        if (this.x > 0)
        {
            neighbours.Add(board[this.x - 1, this.y]);
        }
        return neighbours.Where(n => !n.visited).OrderBy(n => Guid.NewGuid()).FirstOrDefault();
    }

    public Cell(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}