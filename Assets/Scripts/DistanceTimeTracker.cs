using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceTimeTracker : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI timeText;

    private float initialPlayerX;
    [HideInInspector]public float distanceTraveled;
    [HideInInspector]public float gameTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialPlayerX = player.position.x;
        distanceTraveled = 0f;
        gameTime = 0f;
    }

    void Update()
    {
        if (player == null) return;
        distanceTraveled = player.position.x - initialPlayerX;
        distanceText.text = "Distance: " + distanceTraveled.ToString("F2") + "m";

        gameTime += Time.deltaTime;

        timeText.text = "Time: " + Mathf.Floor(gameTime / 60).ToString("00") + ":" + (gameTime % 60).ToString("00");
    }
}
