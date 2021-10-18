using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Prefabs for the enemies
    public GameObject[] enemyPrefabs;
    public GameObject[] bossPrefabs;
    public GameObject[] powerupPrefabs;
    public GameObject enemyRocketPrefab;

    // Range for enemies to spawn
    private float spawnRange = 9;
    
    // Spawn stats
    public int enemyCount;
    public int waveNumber = 0;

    // How many until a boss wave?
    private int bossWaveInterval = 3;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0 && gameManager.isGameActive)
        {
            int randPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randPowerup], powerupPrefabs[randPowerup].transform.position, powerupPrefabs[randPowerup].transform.rotation);

            waveNumber++;
            gameManager.UpdateRound(waveNumber);
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
