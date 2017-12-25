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
    }

    private void Update()
    {
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                BoardManager.DisplayLayer(i);
                break;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            BoardManager.ToggleCollision();
        }
    }
}
