using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefabs;
    public GameObject plantPrefabs;
    public float spawnRate = 30;
    public Transform posA;
    public Transform posB;

    public bool type;
    private void Start()
    {
        type = Random.Range(0, 2) == 0 ? true : false;
        if (type)
        {
            if (Random.Range(0, 100) <= spawnRate)
            {
                GameObject newEnemy = Instantiate(enemyPrefabs, transform);
                newEnemy.transform.position = posA.position;
                EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
                enemyController.posA = posA;
                enemyController.posB = posB;
            }
        } else
        {
            GameObject newEnemy = Instantiate(plantPrefabs, transform);
            newEnemy.transform.position = posB.position;
        }
    }
}
