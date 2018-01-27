using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Tools;
public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mazeBlock;

    [SerializeField]
    private GameObject elevator;

    [SerializeField]
    private Material transparencyMaterial;

    public static int Width;
    public static int Height;
    public static int Levels;

    private GameObject[] lvls;

    private Cell[][,] boards;

    public Vector2 RenderBoard()
    {
        var g = new MazeGenerator();
        boards = new Cell[Levels][,];
        var mazeRoot = new GameObject("Maze");
        lvls = new GameObject[Levels];
        for (int i = 0; i < Levels; i++)
        {
            boards[i] = g.GenerateMaze(Width, Height);
            lvls[i] = new GameObject("Level " + (i + 1));
            lvls[i].transform.SetParent(mazeRoot.transform);
            RenderLevel(lvls[i], i, boards[i]);
        }
        PlaceElevator(Width-1,Height-1);

        return new Vector2(Width*2,Height*2);
    }

    public Texture[] GetMinimapTextures()
    {
        var textures = new Texture2D[Levels];
        for (int lvl = 0; lvl < Levels; lvl++)
        {
            var board = boards[lvl];
            var res = new int[2 * board.GetLength(0) + 1][];
            for (int i = 0; i < res.Length;i++)
            {
                res[i] = new int[2 * board.GetLength(1) + 1];
            }
            for (int i = 0; i < res.GetLength(0); i += 2)
                for (int j = 0; j < res[i].GetLength(0); j += 2)
                {
                    res[i][j] = 1;
                }
            for (var i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    //center
                    res[2 * i + 1][ 2 * j + 1] = 0;
                    //up
                    res[2 * i + 1][ 2 * j] = board[i, j].walls[0] ? 1 : 0;
                    //right
                    res[2 * i + 2][ 2 * j + 1] = board[i, j].walls[1] ? 1 : 0;
                    //down
                    res[2 * i + 1][ 2 * j + 2] = board[i, j].walls[2] ? 1 : 0;
                    //left
                    res[2 * i][ 2 * j + 1] = board[i, j].walls[3] ? 1 : 0;
                }
            }

            var fieldsize = 180;
            var wallSize = 10;

            var texW = board.GetLength(0) * (fieldsize + wallSize) +  wallSize;
            var texH = board.GetLength(1) * (fieldsize + wallSize) +  wallSize;

            Texture2D tex = new Texture2D(texW,texH, TextureFormat.ARGB32, false);
            var color = new[] { new Color(1f,1f,1f,0.1f), new Color(1f, 1f, 1f, .9f) };
            for (int i = 0; i < res.GetLength(0); i++)
                for (int j = 0; j < res[i].GetLength(0); j++)
                {
                    var w = i % 2 == 0 ? wallSize : fieldsize;
                    var h = j % 2 == 0 ? wallSize : fieldsize;

                    var t = (i - (i % 2 )) / 2;
                    var u = (j - (j % 2 )) / 2;

                    var x = t * (wallSize + fieldsize) + (i%2)*wallSize;
                    var y = u * (wallSize + fieldsize) + (j%2)*wallSize;
                    tex.FillRect(x,y,w,h, color[res[i][j]]);
                }
            tex.Apply();
            textures[lvl] = tex;
        }
        return textures;
    }

    public void PlaceElevator(int x, int y)
    {
        var blocks = FindObjectsOfType<FloorScript>().Where(b => b.gameObject.transform.parent.name == "block[" + x + ", " + y + "]").ToList();
        blocks.ForEach(b => b.Destroy());
        Instantiate(elevator, new Vector3(x * 2, 0, -y * 2) + new Vector3(0.5f,0,-0.5f), Quaternion.identity);
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
            var pos = new Vector3(2 * cell.x, level * 2, -2 * cell.y) + new Vector3(0.5f,0,-0.5f);
            var inst = Instantiate(mazeBlock, pos, Quaternion.identity, root.transform);
            inst.name = "block[" + cell.x + ", " + cell.y + "]";
            var behaviour = inst.GetComponent<MazeBlock>();
            behaviour.Init(cell.walls);
        }
    }
}
namespace Tools{

static class TextureExtension
{
    public static void DrawLine(this Texture2D tex, Vector2 p1, Vector2 p2, Color col)
    {
        Vector2 t = p1;
        float frac = 1 / Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2));
        float ctr = 0;

        while ((int)t.x != (int)p2.x || (int)t.y != (int)p2.y)
        {
            t = Vector2.Lerp(p1, p2, ctr);
            ctr += frac;
            tex.SetPixel((int)t.x, (int)t.y, col);
        }
    }

    public static void DrawRect(this Texture2D tex, Vector2 p1, Vector2 p2, Color col)
    {
        float x1 = -1;
        float x2 = -1;
        if (p1.x < p2.x)
        {
            x1 = p1.x;
            x2 = p2.x;
        }
        else
        {
            x1 = p2.x;
            x2 = p1.x;
        }
        float y1 = -1;
        float y2 = -1;
        if (p1.y < p2.y)
        {
            y1 = p1.y;
            y2 = p2.y;
        }
        else
        {
            y1 = p2.y;
            y2 = p1.y;
        }
        p1 = new Vector2(x1, y1);
        p2 = new Vector2(x2, y1);
        var p3 = new Vector2(x2, y2);
        var p4 = new Vector2(x1, y2);
        DrawLine(tex, p1, p2, col);
        DrawLine(tex, p2, p3, col);
        DrawLine(tex, p3, p4, col);
        DrawLine(tex, p4, p1, col);
        Debug.Log(p1);
        Debug.Log(p2);
        Debug.Log(p3);
        Debug.Log(p4);
    }

    public static void FillRect(this Texture2D tex, int startX, int startY, int width, int height, Color col)
        {
        for (int x = startX; x <= startX + width; x++)
        for (int y = startY; y <= startY + height; y++){
            tex.SetPixel(x,y,col);
        }
    }
}
}