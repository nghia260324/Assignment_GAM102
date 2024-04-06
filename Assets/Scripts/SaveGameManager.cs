using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public bool isMenu = false;
    public static SaveGameManager instance;
    private string saveFileName = "save_data.json";
    private string encryptionKey = "Nghiantph41626";
    private int maxSaveCount = 5;
    public GameObject itemHighCore;
    public GameObject content;

    private void Awake()
    {
        instance = this;
        if (isMenu)
        {
            List<GameData> gameDatas = new List<GameData>(LoadGameData());
            for (int i = 0; i < gameDatas.Count; i++)
            {
                GameData gameData = gameDatas[i];
                GameObject newItem = Instantiate(itemHighCore, content.transform);
                newItem.transform.Find("STT").gameObject.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
                newItem.transform.Find("Score").gameObject.GetComponent<TextMeshProUGUI>().text = gameData.highScore.ToString();
            }
        }
    }

    public void SaveGameData(GameData gameData)
    {
        List<GameData> gameDatas = new List<GameData>();
        gameDatas = LoadGameData();
        gameDatas.Insert(0,gameData);
        gameDatas.Sort((x, y) => y.highScore.CompareTo(x.highScore));
        if (gameDatas.Count > maxSaveCount)
        {
            gameDatas.Remove(gameDatas[5]);
        }
        HighScores highScores = new HighScores { scores = gameDatas};
        try
        {
            string json = JsonUtility.ToJson(highScores);
            /*string encryptedData = Encrypt(json, encryptionKey);*/
            string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
            File.WriteAllText(filePath, json);
        }
        catch (System.Exception ex)
        {
            Debug.Log("Loi khi ghi du lieu: " + ex.Message);
        }
    }
    public List<GameData> LoadGameData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
        }
        string json = File.ReadAllText(filePath);
/*        string encryptedData = File.ReadAllText(filePath);
        string json = Decrypt(encryptedData, encryptionKey);*/
        HighScores highScores = JsonUtility.FromJson<HighScores>(json);
        return highScores != null ? highScores.scores : new List<GameData>();
    }
}
[System.Serializable]
public class HighScores
{
    public List<GameData> scores;
}