using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardManager BoardManager;

    private void Start()
    {
        BoardManager.RenderBoard();
        for (int i = 1; i < BoardManager.Levels; i++)
        {
            BoardManager.DisplayLayer(i,false);
        }
        BoardManager.DisplayLayer(0,true);
    }

    public void ShowLevel(int lvlIndex)
    {
        BoardManager.DisplayLayer(lvlIndex, true);
    }

    public void HideLevel(int lvlIndex)
    {
        BoardManager.DisplayLayer(lvlIndex, false);
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
 {
            Camera.main.orthographicSize++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
 {
            Camera.main.orthographicSize--;
        }
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3, 8);
    }
}