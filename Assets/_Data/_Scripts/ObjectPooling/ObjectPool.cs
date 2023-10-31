using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ObjectPooling
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private List<GameObject> objectPrefabs;
        [SerializeField] private int poolSize;
        [SerializeField] private List<GameObject> freeObjects = new();
        [SerializeField] private List<GameObject> usedObjects = new();

        private void Awake()
        {
            for (int i = 0; i < poolSize; i++)
            {
                GenerateNewObject();
            }
        }

        public GameObject GetObjectFromPool()
        {
            GameObject obj = freeObjects.FirstOrDefault(o => o.activeInHierarchy == false);

            if (obj == null)
            {
                obj = GenerateNewObject();
            }

            freeObjects.Remove(obj);
            usedObjects.Add(obj);
            obj.SetActive(true);
            return obj;
        }

        public void ReturnObjectToPool(GameObject obj)
        {
            usedObjects.Remove(obj);
            freeObjects.Add(obj);
            obj.SetActive(false);
        }

        private GameObject GenerateNewObject()
        {
            int randomNum = Random.Range(0, objectPrefabs.Count);
            
            GameObject newObject = Instantiate(objectPrefabs[randomNum], transform);
            newObject.SetActive(false);
            freeObjects.Add(newObject);

            return newObject;
        }
    }
}
