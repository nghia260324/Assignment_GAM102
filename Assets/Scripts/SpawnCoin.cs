using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    public List<GameObject> spawnPositions = new List<GameObject>();
    public GameObject coinPrefabs;
    public GameObject spawnCurve;
    public float spawnRate = 30;

    public int numberOfCoins = 7;
    public float spawnInterval = 0.1f;
    public float maxHeight = 3f;

    private Transform posA;
    private Transform posB;

    private void Awake()
    {
        Transform[] childTransforms = GetComponentsInChildren<Transform>();
        foreach (var childTransform in childTransforms)
        {
            if (childTransform.name == "SpawnPos")
            {
                spawnPositions.Add(childTransform.gameObject);
            }
        }
    }

    private void Start()
    {
        int value = Random.Range(2, spawnPositions.Count);
        if (value > 0)
        {
            for (int i = 0; i < value; i++)
            {
                CreateCoin(spawnPositions[i].transform);
            }
        }

        if (Random.Range(0, 100) <= spawnRate)
        {
            if (spawnCurve == null) return;
            posA = spawnCurve.transform.Find("PosSpawnA").transform;
            posB = spawnCurve.transform.Find("PosSpawnB").transform;
            StartCoroutine(SpawnCoins());
        }
    }
    IEnumerator SpawnCoins()
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            float t = (float)i / (float)(numberOfCoins - 1);
            Vector3 spawnPosition = Vector3.Lerp(posA.position, posB.position, t);

            float curveHeight = Mathf.Sin(t * Mathf.PI) * maxHeight;
            spawnPosition.y += curveHeight;

            GameObject coin = Instantiate(coinPrefabs, transform);
            coin.transform.position = spawnPosition;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void CreateCoin(Transform pos)
    {
        GameObject newCoin = Instantiate(coinPrefabs,transform);
        newCoin.transform.position = pos.position;
        newCoin.name = "Coin";
    }
}
