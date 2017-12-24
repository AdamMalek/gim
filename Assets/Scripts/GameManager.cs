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

    private GameObject lvl1;
    void Start()
    {
        var g = new MazeGenerator();
        var board0 = g.GenerateMaze(5, 5);
        var board1 = g.GenerateMaze(5, 5);
        //var board = new Cell[2,2];
        //board[0, 0] = new Cell(0, 0)
        //{
        //    x = 0,
        //    y = 0,
        //    visited = true,
        //    walls = new[] { true, false, false, true }
        //};
        //board[1, 0] = new Cell(1, 0)
        //{
        //    visited = true,
        //    walls = new[] { true, true, false, false }
        //};
        //board[0, 1] = new Cell(0, 1)
        //{
        //    visited = true,
        //    walls = new[] { false, false, true, true }
        //};
        //board[1, 1] = new Cell(1, 1)
        //{
        //    visited = true,
        //    walls = new[] { false, true, true, false }
        //};

        var mazeRoot = new GameObject ("Maze");
        var lvl0 = new GameObject("Level 0");
        lvl1 = new GameObject("Level 1");

        board0[0, 0].walls[0] = false;
        board1[0, 0].walls[0] = false;

        lvl0.transform.SetParent(mazeRoot.transform);
        lvl1.transform.SetParent(mazeRoot.transform);
        RenderLevel(lvl0, 0, board0);
        RenderLevel(lvl1, 1, board1);
        //corner up-rigth 0
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
        }
    }

    void RenderLevel(GameObject root, int level, Cell[,] board)
    {
        foreach (var cell in board)
        {
            var pos = new Vector3(2 * cell.x, level*2 , -2 * cell.y);
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
            obj.name = "block[" + cell.x + ", " + cell.y + "]";
            var inst = Instantiate(obj, pos, Quaternion.identity, root.transform);
            inst.transform.Rotate(rot);
            //inst.transform.Rotate(new Vector3(0,180,0));
        }
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
