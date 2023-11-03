using System;
using System.Collections.Generic;
using TableAndChair;
using UnityEngine;

namespace Kitchen
{
    public class Cook : MonoBehaviour
    {
        public Transform kitchenPoint;
        public Table targetTable;
        public float time = 0f;
        public bool isDone = false;
        public List<FoodData> foodList;
        public List<FoodData> cookedList;

        private void OnEnable()
        {
            foodList = new List<FoodData>();
            cookedList = new List<FoodData>();
        }

        private void Update()
        {
            if(foodList.Count == 0 && cookedList.Count == 0) return;
            
            if (foodList.Count > 0)
            {
                Cooking(foodList[0]);
            }
            else if (foodList.Count == 0 && cookedList.Count > 0 && !isDone)
            {
                FoodTray foodTray = new FoodTray(targetTable, cookedList);
                
                KitchenManager.Instance.AddFoodTrayList(foodTray);
                isDone = true;
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            targetTable = null;
            time = 0f;
            isDone = false;
            KitchenManager.Instance.ReturnToPool(gameObject);
        }

        private void Cooking(FoodData food)
        {
            time += Time.deltaTime;
            if (time > food.preparationTime)
            {
                time = 0f;
                cookedList.Add(food);
                foodList.Remove(food);
                Debug.Log("Done " + food.name);
            }
        }

        public void AddFood(FoodData food)
        {
            foodList.Add(food);
        }

        public void SetTargetTable(Table table)
        {
            targetTable = table;
        }
    }
}