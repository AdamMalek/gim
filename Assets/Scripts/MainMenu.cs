using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private int levelNum = 2;

    [SerializeField]
    private int heightNum = 1;

    [SerializeField]
    private int widthNum = 1;

    [SerializeField]
    private int timeNum = 1;

    void OnGUI()
    {
        var level = GUI.TextField(new Rect(Screen.width / 1.6f, Screen.height * 0.2f, Screen.width * 0.04f, Screen.height / 10),
            levelNum.ToString());

        var height = GUI.TextField(new Rect(Screen.width / 1.6f, Screen.height * 0.3f, Screen.width * 0.04f, Screen.height / 10),
            heightNum.ToString());

        var width = GUI.TextField(new Rect(Screen.width / 1.6f, Screen.height * 0.4f, Screen.width * 0.04f, Screen.height / 10),
            widthNum.ToString());

        //var time = GUI.Slider(new Rect(Screen.width / 1.6f, Screen.height * 0.4f, Screen.width * 0.04f, Screen.height / 10),
        //    widthNum.ToString());

        if (GUI.Button(new Rect(Screen.width / 2.5f, Screen.height * 0.1f, Screen.width / 5, Screen.height / 10),
            "Start Game"))
        {
            BoardManager.Levels = levelNum;
            BoardManager.Height = heightNum;
            BoardManager.Width = widthNum;

            SceneManager.LoadScene(1);
        }

        if (GUI.Button(new Rect(Screen.width / 2.5f, Screen.height * 0.2f, Screen.width / 5, Screen.height / 10),
            "Levels"))
        {
            levelNum += 1;
            if (levelNum == 10)
                levelNum = 1;

            level = GUI.TextField(new Rect(Screen.width / 1.6f, Screen.height * 0.2f, Screen.width * 0.04f, Screen.height / 10),
                levelNum.ToString());
        }

        if (GUI.Button(new Rect(Screen.width / 2.5f, Screen.height * 0.3f, Screen.width / 5, Screen.height / 10),
            "Height"))
        {
            heightNum += 1;
            if (heightNum == 10)
                heightNum = 1;

            height = GUI.TextField(new Rect(Screen.width / 1.6f, Screen.height * 0.3f, Screen.width * 0.04f, Screen.height / 10),
                heightNum.ToString());
        }

        if (GUI.Button(new Rect(Screen.width / 2.5f, Screen.height * 0.4f, Screen.width / 5, Screen.height / 10),
            "Width"))
        {
            widthNum += 1;
            if (widthNum == 10)
                widthNum = 1;

            width = GUI.TextField(new Rect(Screen.width / 1.6f, Screen.height * 0.4f, Screen.width * 0.04f, Screen.height / 10),
                widthNum.ToString());
        }

        if (GUI.Button(new Rect(Screen.width / 2.5f, Screen.height * 0.5f, Screen.width / 5, Screen.height / 10),
            "Exit"))
        {
            Application.Quit();
        }
    }
}
