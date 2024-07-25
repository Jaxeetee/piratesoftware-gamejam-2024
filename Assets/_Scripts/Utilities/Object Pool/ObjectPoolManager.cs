using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyUtils
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        private Dictionary<string, PooledObjectItem> pooledDictionary = new Dictionary<string, PooledObjectItem>();

        public void CreatePool(string key, GameObject item, GameObject parent = null, int amtOfCopies = 10)
        {
            pooledDictionary.Add(key, new PooledObjectItem(item, parent, amtOfCopies));
        }

        public GameObject GetObject(string key)
        {
            
            return pooledDictionary.TryGetValue(key, out var pool) ? pool.GetObject() : null;
        }

        public void ReturnToPool(string key, GameObject returnObject)
        {
            if (pooledDictionary.TryGetValue(key, out var pool))
            {
                pool.ReturnToPool(returnObject);
            }
        }

    }

}
