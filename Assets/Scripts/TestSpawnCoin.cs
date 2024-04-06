using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnCoin : MonoBehaviour
{
    public GameObject objectToSpawn;
    public int numberOfCoins = 10;
    public float spawnInterval = 0.1f;
    public Transform posA;
    public Transform posB;
    public float maxHeight = 5f;

    private void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            float t = (float)i / (float)(numberOfCoins - 1);
            Vector3 spawnPosition = Vector3.Lerp(posA.position, posB.position, t);

            float curveHeight = Mathf.Sin(t * Mathf.PI) * maxHeight;
            spawnPosition.y += curveHeight;

            GameObject coin = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
