using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public List<string> spawnList;

    private ObjectPooler pool;
    private float mapBounds = 16.0f; // Bounds of square map

    // Start is called before the first frame update
    void Start()
    {
        List<string> spawnList = new();
        pool = GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>();

        Invoke("ReadPool", 2);

        InvokeRepeating("SpawnEnemyAtRandomPos", 5f, 0.5f);
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn random enemy at random position in bounds of map
    public void SpawnEnemyAtRandomPos()
    {
        int randomEnemy = Random.Range(0, spawnList.Count);
        float randomXpos = Random.Range(-mapBounds, mapBounds);
        float randomYpos = Random.Range(-mapBounds, mapBounds);
        Vector3 randPos = new Vector3(randomXpos, 0.3f, randomYpos);

        pool.SpawnFromPool(spawnList[randomEnemy], randPos, Quaternion.identity);

    }

    // Read from pool tags and write to spawnlist
    private void ReadPool()
    {
        foreach (ObjectPooler.Pool pooles in pool.pools)
        {
            spawnList.Add(pooles.tag);
        }
        
    }
}
