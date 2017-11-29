using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject NoWalls;
    public GameObject singleWall;
    public GameObject twoWalls;
    public GameObject corner;
    public GameObject threeWalls;
    void Start()
    {
        var g = new MazeGenerator();
        var board = g.GenerateMaze(10, 10);
        //var board = new Cell[2, 1];
        //board[0, 0] = new Cell(0, 0)
        //{
        //    x = 0,
        //    y = 0,
        //    visited = true,
        //    walls = new[] { true, true, true, false }
        //};
        //board[1, 0] = new Cell(1, 0)
        //{
        //    visited = true,
        //    walls = new[] { true, true, true, false }
        //};
        //board[0, 1] = new Cell(0, 1)
        //{
        //    visited = true,
        //    walls = new[] { true, false, false, true }
        //};
        //board[1, 1] = new Cell(1, 1)
        //{
        //    visited = true,
        //    walls = new[] { true, true, false, false }
        //};
        foreach (var cell in board)
        {
            var pos = new Vector3(2 * cell.x, 0, 2 * cell.y);
            var rot = getRotation(cell);
            GameObject obj = null;
            switch (cell.walls.Count(x => x))
            {
                case 0:
                    obj = NoWalls;
                    break;
                case 1:
                    obj = singleWall;
                    break;
                case 2:
                    obj = cell.walls[0] == cell.walls[2] ? twoWalls : corner;
                    break;
                case 3:
                    obj = threeWalls;
                    break;
            }
            var inst = Instantiate(obj, pos, Quaternion.identity);
            inst.transform.Rotate(rot);
            //inst.transform.Rotate(new Vector3(0,180,0));
        }
        //corner up-rigth 0
    }

    private Vector3 getRotation(Cell cell)
    {
        var walls = cell.walls.Count(x => x);
        //corner up-rigth 0
        switch (walls)
        {
            case 1:
                if (cell.walls[0]) return new Vector3(0, -90, 0);
                if (cell.walls[1]) return new Vector3(0, 0, 0);
                return cell.walls[2] ? new Vector3(0, 90, 0) : new Vector3(0, 180, 0);
            case 2:
                if (cell.walls[0] != cell.walls[2])
                {
                    if (cell.walls[0])
                    {
                        if (cell.walls[1]) return new Vector3(0, -90, 0);
                        return new Vector3(0, 180, 0);
                    }
                    if (cell.walls[1]) return new Vector3(0, 0, 0);
                    return new Vector3(0, 90f, 0);
                }
                if (cell.walls[0]) return new Vector3(0, 90, 0);
                return new Vector3(0, 0, 0);
            case 3:
                if (!cell.walls[0]) return new Vector3(0, 180, 0);
                if (!cell.walls[1]) return new Vector3(0, -90, 0);
                return !cell.walls[2] ? new Vector3(0, 0, 0) : new Vector3(0, 90, 0);
            default:
                return new Vector3(0, 0, 0);
        }
    }
}
