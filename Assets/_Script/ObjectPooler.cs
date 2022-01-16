using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singletone<ObjectPooler>
{
    public List<Pool> pools;
    public Dictionary<PoolTag, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<PoolTag, Queue<GameObject>>();

        foreach (var item in pools)
        {
            Queue<GameObject> objectsPool = new Queue<GameObject>();

            for (int i = 0; i < item.size; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                objectsPool.Enqueue(obj);
            }

            poolDictionary.Add(item.poolTag, objectsPool);
        }
    }

    public GameObject SpawnFromPool(PoolTag tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError($"Pool doesn`t excist (tag: {tag}).");
            return null;
        }

        GameObject objPoSpawn = poolDictionary[tag].Dequeue();

        objPoSpawn.transform.position = position;
        objPoSpawn.transform.rotation = rotation;
        objPoSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objPoSpawn);

        return objPoSpawn;
    }
}

public class LocalPool
{
    int createMore;
    GameObject prefab;
    Transform parentTransform;
    Stack<GameObject> pool;

    public LocalPool(Transform parent, GameObject prefab, int size, int createMore = 5)
    {
        this.createMore = createMore;
        this.prefab = prefab;
        parentTransform = parent;
        pool = new Stack<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject obj = Object.Instantiate(this.prefab, parent);
            obj.SetActive(false);
            pool.Push(obj);
        }
    }

    public GameObject PoolObjectGet(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            CreateMore();
        }

        GameObject objPoSpawn = pool.Pop();

        objPoSpawn.transform.position = position;
        objPoSpawn.transform.rotation = rotation;
        objPoSpawn.SetActive(true);

        return objPoSpawn;
    }

    public void PoolObjectSet(GameObject obj)
    {
        pool.Push(obj);
        obj.SetActive(false);
    }

    void CreateMore()
    {
        for (int i = 0; i < createMore; i++)
        {
            GameObject obj = Object.Instantiate(this.prefab, parentTransform);
            obj.SetActive(false);
            pool.Push(obj);
        }
    }
}

[System.Serializable]
public class Pool
{
    public PoolTag poolTag;
    public GameObject prefab;
    public int size;
}

public enum PoolTag
{
    
}
