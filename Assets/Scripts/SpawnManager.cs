using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] bossPrefabs;
    public GameObject[] powerupPrefabs;
    public GameObject enemyRocketPrefab;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;

    private int bossWaveInterval = 3;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyPrefabs[0], GenerateSpawnPosition(), enemyPrefabs[0].transform.rotation);
        Instantiate(powerupPrefabs[0], GenerateSpawnPosition(), powerupPrefabs[0].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0)
        {
            int randPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randPowerup], powerupPrefabs[randPowerup].transform.position, powerupPrefabs[randPowerup].transform.rotation);

            waveNumber++;
            // If the interval has passed summon boss ow summon spawns
            if (waveNumber % bossWaveInterval == 0)
            {
                SpawnBossWave();
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return spawnPos;
    }

    // Spawn enemy waves
    public void SpawnEnemyWave(int enemiesToSpawn)
    {
        int randEnemy;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            randEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randEnemy], GenerateSpawnPosition(), enemyPrefabs[randEnemy].transform.rotation);
        }
    }

    private void SpawnBossWave()
    {
        int randBoss = Random.Range(0, bossPrefabs.Length);

        Instantiate(bossPrefabs[randBoss], GenerateSpawnPosition(), bossPrefabs[randBoss].transform.rotation);
    }

}
