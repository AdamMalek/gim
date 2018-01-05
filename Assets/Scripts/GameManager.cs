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

    }
}
