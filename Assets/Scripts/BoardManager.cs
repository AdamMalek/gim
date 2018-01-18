using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mazeBlock;
    
    [SerializeField]
    private GameObject elevator;

    [SerializeField]
    private Material transparencyMaterial;

    public int Width;
    public int Height;
    public int Levels;

    private GameObject[] lvls;
    public void RenderBoard()
    {
        var g = new MazeGenerator();
        var boards = new Cell[Levels][,];
        var mazeRoot = new GameObject("Maze");
        lvls = new GameObject[Levels];
        for (int i = 0; i < Levels; i++)
        {
            boards[i] = g.GenerateMaze(Width, Height);
            lvls[i] = new GameObject("Level " + (i + 1));
            lvls[i].transform.SetParent(mazeRoot.transform);
            RenderLevel(lvls[i], i, boards[i]);
        }
        //PlaceElevator(Width-1,Height-1);
    }

    public void PlaceElevator(int x, int y)
    {
        FindObjectsOfType<FloorScript>().Where(b => b.name == "block[" + x + ", " + y + "]").ToList().ForEach(b => b.Hide());
        Instantiate(elevator, new Vector3(x * 2, 0, -y * 2), Quaternion.identity);
    }

    public void DisplayLayer(int index, bool display)
    {
        if (index < 0 || index >= lvls.Length) return;
        var layer = lvls[index];
        if (layer != null)
        {
            layer.SetActive(display);
        }
    }

    void RenderLevel(GameObject root, int level, Cell[,] board)
    {
        foreach (var cell in board)
        {
            var pos = new Vector3(2 * cell.x, level * 2, -2 * cell.y);
            var inst = Instantiate(mazeBlock, pos, Quaternion.identity, root.transform);
            inst.name = "block[" + cell.x + ", " + cell.y + "]";
            var behaviour = inst.GetComponent<MazeBlock>();
            behaviour.Init(cell.walls);
        }
    }
}
