using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitCamera : MonoBehaviour
{
    public float distance;
    private Transform player;
    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        } else
        {
            transform.position = new Vector2(player.position.x + distance, transform.position.y);
        }
    }
}
