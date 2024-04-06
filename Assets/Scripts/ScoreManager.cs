using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int currentCoin;
    public int hightScore;

    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateText();
    }

    public void UpdateCoinAndScore(int coin)
    {
        currentCoin += coin;
        hightScore += coin;
        UpdateText();
    }
    public void DecreaseCoins(int coin)
    {
        currentCoin -= coin;
        UpdateText();
    }
    private void UpdateText()
    {
        highscoreText.text = "HightCore: " + hightScore;
        coinText.text = currentCoin.ToString();
    }
}
