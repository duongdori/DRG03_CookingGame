using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace ObjectPooling
{
    [Serializable]
    public class ObjectPool<T> where T : MonoBehaviour
    {
        public List<T> objectPrefabs;
        public int poolSize;
        public Transform parentHolder;
        public List<T> freeObjects = new();
        public List<T> usedObjects = new();

        public ObjectPool(List<T> prefabs, int size, Transform parent)
        {
            objectPrefabs = prefabs;
            poolSize = size;
            parentHolder = parent;
        }
        public void Setup()
        {
            for (int i = 0; i < poolSize; i++)
            {
                GenerateNewObject();
            }
        }

        public T GetObjectFromPool()
        {
            T obj = freeObjects.FirstOrDefault(o => o.gameObject.activeInHierarchy == false);

            if (obj == null)
            {
                obj = GenerateNewObject();
            }

            freeObjects.Remove(obj);
            usedObjects.Add(obj);
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void ReturnObjectToPool(T obj)
        {
            usedObjects.Remove(obj);
            freeObjects.Add(obj);
            obj.gameObject.SetActive(false);
        }

        private T GenerateNewObject()
        {
            int randomNum = Random.Range(0, objectPrefabs.Count);
            
            T newObject = Object.Instantiate(objectPrefabs[randomNum], parentHolder);
            newObject.gameObject.SetActive(false);
            freeObjects.Add(newObject);

            return newObject;
        }
    }
}
