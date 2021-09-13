using System.Collections;
using UnityEngine;

public class Summoner : MonoBehaviour
{
    SpawnManager spawnManager;
    private float spawnInterval = 5;
    private int numberOfEnemies = 3;

    void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        StartCoroutine(SpawnSubEnemies());
    }

    IEnumerator SpawnSubEnemies()
    {
        while (gameObject != null)
        {
            yield return new WaitForSeconds(spawnInterval);
            spawnManager.SpawnEnemyWave(Random.Range(0, numberOfEnemies + 1));
        }
    }
}
