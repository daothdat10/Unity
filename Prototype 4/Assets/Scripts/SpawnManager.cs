using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] powerupPrefabs;

    public int enemyCount;
    public int waveNumber = 1;

    private float spawnX;
    private float spawnZ;

    //boss
    public GameObject bossPrefabs;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;

    private float spawnRange = 9;
    // Start is called before the first frame update
    void Start()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup],GenerateSpawnPosition(),powerupPrefabs[randomPowerup].transform.rotation);

        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {

            waveNumber++;
            SpawnEnemyWave(waveNumber);

            if(waveNumber % bossRound == 0)
            {
                SpawnBossWare(waveNumber);
            }else
            {
                SpawnEnemyWave(waveNumber);
            }

            int randomPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {

        spawnX = Random.Range(-spawnRange, spawnRange);
        spawnZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnX, 0, spawnZ);

        return randomPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i = 0;i< enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomEnemy], GenerateSpawnPosition(), enemyPrefab[randomEnemy].transform.rotation);
        }
    }
    void SpawnBossWare(int currentRount)
    {
        int miniEnemysToSpawn;
        if(bossRound != 0)
        {
            miniEnemysToSpawn = currentRount / bossRound;
        }else
        {
            miniEnemysToSpawn = 1;
        }
        var boss = Instantiate(bossPrefabs,GenerateSpawnPosition(),bossPrefabs.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemysToSpawn;
    }
    public void SpawnMiniEnemy(int amount)
    {
        for(int i = 0; i< amount; i++)
        {
            int randomMini = Random.Range(0,miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPosition(), miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }
}
