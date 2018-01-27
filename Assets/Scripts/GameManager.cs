using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using Tools;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static int timeLimit = 20;
    TimeSpan TimeLimit;

    [SerializeField]
    UnityEngine.UI.Text timeText;

    [SerializeField]
    Transform playerCharacter;

    [SerializeField]
    private UnityEngine.UI.RawImage miniMap;
    [SerializeField]
    private UnityEngine.UI.Image playerMarker;

    private Texture[] textures;
    public BoardManager BoardManager;

    private Vector2 boardSize;
    private Vector2 miniMapSize;
    private void Start()
    {
        TimeLimit = TimeSpan.FromSeconds(timeLimit);
        boardSize = BoardManager.RenderBoard();
        for (int i = 1; i < BoardManager.Levels; i++)
        {
            BoardManager.DisplayLayer(i,false);
        }
        textures = BoardManager.GetMinimapTextures();
        ShowLevel(0);
        miniMapSize = new Vector2(miniMap.uvRect.width,miniMap.uvRect.height);
    }

    public void ShowLevel(int lvlIndex)
    {
        BoardManager.DisplayLayer(lvlIndex, true);
        miniMap.texture = textures[lvlIndex];
    }

    public void HideLevel(int lvlIndex)
    {
        BoardManager.DisplayLayer(lvlIndex, false);
    }
    int sec = -1;
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

        TimeLimit = TimeLimit.Subtract(TimeSpan.FromSeconds(Time.deltaTime));
        if (TimeLimit <= TimeSpan.Zero){
            FindObjectsOfType<FallScript>().ToList().ForEach(x=> x.EnableFalling());
        }
        else if (TimeLimit.Seconds != sec){
            timeText.text = TimeLimit.Minutes + ":" + TimeLimit.Seconds.ToString().PadLeft(2,'0');
        }
        sec = TimeLimit.Seconds;
        var px = (playerCharacter.position.x + 0.5f)/boardSize.x;
        var py = -(playerCharacter.position.z - 0.5f)/boardSize.y;
        Debug.Log("Npos: " + px + ", " + py);
        Debug.Log("Ppos: " + px * 250 + ", " + py * 250);
        // playerMarker.gameObject.GetComponent<RectTransform>().localPosition = new Vector2(px * 250, (1-py) *  250);
        playerMarker.gameObject.GetComponent<RectTransform>().localPosition = new Vector2((px-0.5f)*250,(py-0.5f)*250);
        playerMarker.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,-playerCharacter.eulerAngles.y);
    }
}