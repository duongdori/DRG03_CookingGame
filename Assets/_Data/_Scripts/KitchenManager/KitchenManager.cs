using System;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

namespace Kitchen
{
    public class KitchenManager : MonoBehaviour
    {
        public static KitchenManager Instance { get; private set; }

        [SerializeField] private List<Cook> cookPrefabs;
        [SerializeField] private ObjectPool<Cook> objectPool;
        
        public Transform kitchenPoint;
        [SerializeField] private List<FoodTray> foodTrayList = new();

        private void Awake()
        {
            if (Instance == null) Instance = this;

            objectPool = new ObjectPool<Cook>(cookPrefabs, 5, transform);
            objectPool.Setup();
        }

        public Cook CreateCook()
        {
            Cook cook = objectPool.GetObjectFromPool();
            if (cook == null) return null;
            
            cook.SetupCook();
            return cook;
        }

        public void AddFoodTrayList(FoodTray foodTray)
        {
            foodTrayList.Add(foodTray);
        }

        public FoodTray GetFoodTray()
        {
            if (foodTrayList.Count == 0) return null;

            FoodTray foodTray = foodTrayList[0];
            return foodTray;
        }

        public void RemoveFoodTray()
        {
            foodTrayList.RemoveAt(0);
        }

        public void ReturnToPool(Cook obj)
        {
            objectPool.ReturnObjectToPool(obj);
        }
    }
}
