using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

namespace Kitchen
{
    public class KitchenManager : MonoBehaviour
    {
        public static KitchenManager Instance { get; private set; }

        [SerializeField] private ObjectPool objectPool;
        
        public Transform kitchenPoint;
        [SerializeField] private List<FoodTray> foodTrayList = new();

        private void Awake()
        {
            if (Instance == null) Instance = this;

            objectPool = GetComponent<ObjectPool>();
        }

        public Cook CreateCook()
        {
            GameObject cookObj = objectPool.GetObjectFromPool();
            Cook cook = cookObj.GetComponent<Cook>();
            if (cook == null) return null;
            
            cook.kitchenPoint = kitchenPoint;
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

        public void ReturnToPool(GameObject obj)
        {
            objectPool.ReturnObjectToPool(obj);
        }
    }
}
