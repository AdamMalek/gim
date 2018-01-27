using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public int height;
    public int width;
    public int timeLimit;

    public void Start()
    {
        GetComponentsInChildren<Text>()[0].text = height.ToString();
        GetComponentsInChildren<Text>()[1].text = width.ToString();
        GetComponentsInChildren<Text>()[2].text = timeLimit.ToString();
    }

    public void SetHeight(float x)
    {
        height = (int)x;
        GetComponentsInChildren<Text>()[0].text = height.ToString();
    }

    public void SetWidth(float x)
    {
        width = (int)x;
        GetComponentsInChildren<Text>()[1].text = width.ToString();
    }

    public void SetTimeLimit(float x)
    {
        timeLimit = (int)x;
        GetComponentsInChildren<Text>()[2].text = timeLimit.ToString();
    }

    public void StartGame()
    {
        BoardManager.Levels = 2;
        BoardManager.Height = height;
        BoardManager.Width = width;
        GameManager.timeLimit = timeLimit;

        SceneManager.LoadScene(1);
    }
}
