using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MazeGenerator
{
    private Cell[,] _board;
    private Stack<Cell> _stack;

    private void InitGenerator(int rows, int cols)
    {
        _board = new Cell[cols, rows];
        _stack = new Stack<Cell>();
        for (var i = 0; i < cols; i++)
        {
            for (var j = 0; j < rows; j++)
            {
                _board[i, j] = new Cell(i, j);
            }
        }
    }

    public Cell[,] GenerateMaze(int rows, int cols)
    {
        InitGenerator(rows,cols);
        var current = _board[0, 0];
        current.visited = true;
        while (current != null)
        {
            var next = current.PickNeighbour(_board);
            if (next != null)
            {
                next.visited = true;
                _stack.Push(current);

                RemoveWalls(current, next);

                current = next;
            }
            else if (_stack.Count > 0)
            {
                current = _stack.Pop();
            }
            else
            {
                current = null;
            }
        }
        return _board;
    }

    private void RemoveWalls(Cell c1, Cell c2)
    {
        if (c1.y > c2.y)
        {
            c1.walls[0] = c2.walls[2] = false;
        }
        else if (c1.y < c2.y)
        {
            c1.walls[2] = c2.walls[0] = false;
        }
        else if (c1.x > c2.x)
        {
            c1.walls[3] = c2.walls[1] = false;
        }
        else if (c1.x < c2.x)
        {
            c1.walls[1] = c2.walls[3] = false;
        }
    }
}
