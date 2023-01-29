using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [System.Serializable]                                                            
    public class Pool                                                                // New Class for Pool data 
    {                                                                                // 
        public string tag;                                                           // Object tag
        public GameObject prefab;                                                    // Object prefab
        public int size;                                                             // Number of prefab with tag save to pool
    }                                                                                // 
                                                                                     // 
    public List<Pool> pools;                                                         // List of objects pool
    public Dictionary<string, Queue<GameObject>> poolDictionary;                     // Dictionary for saving queues of objects in pool


    
    
    // Start is called before the first frame update
    void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // Create pool of objects
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // Method for spawning by tag from pool at specific position
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

}
