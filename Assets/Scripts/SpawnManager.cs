using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SpawnManager : MonoBehaviour
{
    
    public ObjectPooler pool;                  // Pointer to pool
    public float enemySpawnRate = 1.0f;        // Spawning rate of enemies
    public int bonusSpawnRate;                 // Spawning rate of standart bonuses
    public int rareBonusSpawnRate;             // Spawning rate of rare bonuses
    public int weaponSpawntRate;               // Spawning rate of weapons
                                                
    private List<string> enemySpawnList;       // Enemy spawn list
    private List<string> bonusSpawnList;       // Standart bonuses spawn list
    private List<string> rareBonusSpawnList;   // Rare bonuses spawn list
    private readonly float mapBounds = 16.0f;  // Bounds of square map
    private bool readDone = false;             // Confirmation of pool read done
    private GameManager GM;                    // 
    private string weaponDrop;                 // Tag name of weapon's drop in pool dictionary
    private int bonusVar;                      // Helping variable for standart bonus spawn countdawn
    private int rareVar;                       // Helping variable for rare bonus spawn countdawn
    private int weaponVar;                     // Helping variable for weapons spawn countdawn

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnList = new();
        bonusSpawnList = new();
        rareBonusSpawnList = new();

        GM = GameManager.Instance;

        if (pool != null)
        {
            Invoke(nameof(ReadPool), 2);
        }
        else { Debug.LogError("Assign ObjectPooler to SpawnManager"); }

        StartCoroutine(SpawnWithRate(SpawnRandomEnemyAtRandomPos, enemySpawnRate));
        bonusVar = bonusSpawnRate;
        rareVar = rareBonusSpawnRate;
        weaponVar = weaponSpawntRate;        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(bonusVar + " " + rareVar + " " + weaponVar);
        if (readDone)
        {
            if (GM.totalKills >= bonusVar)
            {
                bonusVar = GM.totalKills + bonusSpawnRate;
                SpawnRandomBonusAtRandomScreenPos();
            }

            if (GM.totalKills >= rareVar)
            {
                rareVar = GM.totalKills + rareBonusSpawnRate;
                SpawnRandomRareBonusAtRandomScreenPos();
            }

            if (GM.totalKills >= weaponVar)
            {
                weaponVar = GM.totalKills + weaponSpawntRate;
                SpawnWeaponAtRandomScreenPos();
            }

        }
    }

    // Spawn random enemy at random position in bounds of map
    public void SpawnRandomEnemyAtRandomPos()
    {
        int randomEnemy = UnityEngine.Random.Range(0, enemySpawnList.Count);
        float randomXpos = UnityEngine.Random.Range(-mapBounds, mapBounds);
        float randomYpos = UnityEngine.Random.Range(-mapBounds, mapBounds);
        Vector3 randPos = new Vector3(randomXpos, 0.1f, randomYpos);

        pool.SpawnFromPool(enemySpawnList[randomEnemy], randPos, Quaternion.identity);

    }

    public void SpawnRandomBonusAtRandomScreenPos()
    {
        int randomBonus = UnityEngine.Random.Range(0, bonusSpawnList.Count);
        
        pool.SpawnFromPool(bonusSpawnList[randomBonus], RandomScreenToWorldPos(), Quaternion.identity);
    }

    public void SpawnRandomRareBonusAtRandomScreenPos()
    {
        int randomBonus = UnityEngine.Random.Range(0, rareBonusSpawnList.Count);
        
        pool.SpawnFromPool(rareBonusSpawnList[randomBonus], RandomScreenToWorldPos(), Quaternion.identity);
    }

    public void SpawnWeaponAtRandomScreenPos()
    {
        pool.SpawnFromPool(weaponDrop, RandomScreenToWorldPos(), Quaternion.identity);
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
            if (pooles.tag.Contains("Rare"))
            {
                rareBonusSpawnList.Add(pooles.tag);
            }
            if (pooles.tag.Contains("Weapon"))
            {
                weaponDrop = pooles.tag;
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

    // Return random position on Ground in Camera view field
    private Vector3 RandomScreenToWorldPos()
    {
        float randomXpos = UnityEngine.Random.Range(0.1f, 0.9f);
        float randomYpos = UnityEngine.Random.Range(0.1f, 0.9f);
        Vector3 worldRandPos = Camera.main.ViewportToWorldPoint(new Vector3(randomXpos, randomYpos, 22.9f ));
        return worldRandPos;
    }
}
