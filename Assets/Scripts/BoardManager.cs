using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    [SerializeField]
    private GameObject NoWalls;
    [SerializeField]
    private GameObject singleWall;
    [SerializeField]
    private GameObject twoWalls;
    [SerializeField]
    private GameObject corner;
    [SerializeField]
    private GameObject threeWalls;
    [SerializeField]
    private GameObject elevator;

    [SerializeField]
    private Material transparencyMaterial;

    public int Width;
    public int Height;
    public int Levels;

    private GameObject[] lvls;
    public void  RenderBoard()
    {
        var g = new MazeGenerator();
        var boards = new Cell[Levels][,];
        var mazeRoot = new GameObject("Maze");
        lvls = new GameObject[Levels];
        for (int i = 0; i < Levels; i++)
        {
            boards[i] = g.GenerateMaze(Width, Height);
            lvls[i] = new GameObject("Level "+ (i+1));
            lvls[i].transform.SetParent(mazeRoot.transform);
            RenderLevel(lvls[i], i, boards[i]);
        }
        PlaceElevator(Width-1,Height-1);
    }

    public void PlaceElevator(int x,int y)
    {
        FindObjectsOfType<BlockBehaviour>().Where(b => b.name == "block[" + x + ", " + y + "]").ToList().ForEach(b=> b.setFloorVisibility(false));
        Instantiate(elevator, new Vector3(x*2, 0, -y*2), Quaternion.identity);
    }

    public void DisplayLayer(int index,bool display)
    {
        if (index < 0 || index >= lvls.Length) return;
        var layer = lvls[index];
        if (layer != null)
        {
            layer.SetActive(display);
        }
    }

    public void ToggleCollision()
    {
        foreach (var layer in lvls)
        {
            Enumerable.Range(0, layer.transform.childCount)
                    .Select(x => layer.transform.GetChild(x))
                    .ToList().ForEach(x =>
                    {
                        var behaviour = x.GetComponent<BlockBehaviour>();
                        behaviour.setCollisions(!behaviour.Collisions);
                    });
        }
    }

    void RenderLevel(GameObject root, int level, Cell[,] board)
    {
        foreach (var cell in board)
        {
            var pos = new Vector3(2 * cell.x, level * 2, -2 * cell.y);
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
            var inst = Instantiate(obj, pos, Quaternion.identity, root.transform);
            inst.name = "block[" + cell.x + ", " + cell.y + "]";
            inst.transform.Rotate(rot);
            var behaviour = inst.GetComponent<BlockBehaviour>();
            behaviour.Init(transparencyMaterial);
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
