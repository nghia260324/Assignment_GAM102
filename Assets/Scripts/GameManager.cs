using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject displayPause;
    public GameObject displayEnd;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI hightScoreText;
    private bool isPause = false;
    private void Awake()
    {
        instance = this;
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        isPause = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPause = false;
        Time.timeScale = 1f;
    }
    public void SaveData(float distance,float time, float hightScore)
    {
        GameData newGameData = new GameData(distance, time, hightScore);
        SaveGameManager.instance.SaveGameData(newGameData);
        displayEnd.SetActive(true);
        timeText.text = "Time: " + Mathf.Floor(time / 60).ToString("00") + ":" + (time % 60).ToString("00");
        distanceText.text = "Distance: " + distance.ToString("F2") + "m";
        hightScoreText.text = "Hightscore: " + hightScore.ToString();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause) { 
            PauseGame();
            displayPause.SetActive(true);
        }
    }
}
