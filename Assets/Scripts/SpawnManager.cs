using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SpawnManager : MonoBehaviour
{
    
    public ObjectPooler pool;
    public float enemySpawnRate = 1.0f;

    private List<string> enemySpawnList;
    private List<string> bonusSpawnList;
    private List<string> weaponSpawnList;
    private readonly float mapBounds = 16.0f; // Bounds of square map
    private bool readDone = false; // Confirmation of pool read done

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnList = new();
        bonusSpawnList = new();
        
        if (pool != null)
        {
            Invoke(nameof(ReadPool), 2);
        }
        else { Debug.LogError("Assign ObjectPooler to SpawnManager"); }

        StartCoroutine(SpawnWithRate(SpawnRandomEnemyAtRandomPos, enemySpawnRate));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn random enemy at random position in bounds of map
    public void SpawnRandomEnemyAtRandomPos()
    {
        int randomEnemy = UnityEngine.Random.Range(0, enemySpawnList.Count);
        float randomXpos = UnityEngine.Random.Range(-mapBounds, mapBounds);
        float randomYpos = UnityEngine.Random.Range(-mapBounds, mapBounds);
        Vector3 randPos = new Vector3(randomXpos, 0.3f, randomYpos);

        pool.SpawnFromPool(enemySpawnList[randomEnemy], randPos, Quaternion.identity);

    }

    // Read from pool tags and write to spawnlist
    private void ReadPool()
    {
        foreach (ObjectPooler.Pool pooles in pool.pools)
        {
            if (pooles.tag.Contains("Enemy"))
            {
                enemySpawnList.Add(pooles.tag);
            }
            if (pooles.tag.Contains("Bonus"))
            {
                bonusSpawnList.Add(pooles.tag);
            }
            if (pooles.tag.Contains("Weapon"))
            {
                weaponSpawnList.Add(pooles.tag);
            }
        }
        readDone = true;

    }

    // Spawn enemy with specific rate
    IEnumerator SpawnWithRate(Action SpawnMethod, float rate)
    {
        if (readDone)
        {
            SpawnMethod();
        }
        yield return new WaitForSeconds(rate);

        StartCoroutine(SpawnWithRate(SpawnRandomEnemyAtRandomPos, enemySpawnRate));
    }
}
