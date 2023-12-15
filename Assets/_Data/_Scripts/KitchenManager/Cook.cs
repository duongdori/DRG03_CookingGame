using System;
using System.Collections;
using System.Collections.Generic;
using TableAndChair;
using UnityEngine;

namespace Kitchen
{
    public class Cook : MonoBehaviour
    {
        public Table targetTable;
        public FoodTray foodTray;
        public float time = 0f;
        public bool isDone = false;
        public List<FoodData> foodList = new List<FoodData>();
        public List<FoodData> cookedList = new List<FoodData>();

        private void Update()
        {
            if (foodList.Count == 0 && cookedList.Count == 0) return;
            if (foodList.Count > 0)
            {
                Cooking(foodList[0]);
            }
            else
            {
                if(isDone) return;
                foreach (var food in cookedList)
                {
                    foodTray.foodList.Add(food);
                }
                
                KitchenManager.Instance.AddFoodTrayList(foodTray);
                cookedList.Clear();
                foodList.Clear();
                isDone = true;
                foodTray = null;
                ReturnToPool();
            }
        }
        
        public void SetupCook()
        {
            //foodList.Clear();
            //cookedList.Clear();
        }

        private void ReturnToPool()
        {
            targetTable = null;
            time = 0f;
            isDone = false;
            KitchenManager.Instance.ReturnToPool(this);
        }

        private void Cooking(FoodData food)
        {
            time += Time.deltaTime;
            if (time >= food.preparationTime)
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
            foodTray = new FoodTray(targetTable);
        }
    }
}