﻿using System;
using System.Collections.Generic;
using System.Linq;
using TableAndChair;
using UnityEngine;

namespace Kitchen
{
    [Serializable]
    public class FoodTray
    {
        public Table targetTable;
        public List<FoodData> foodList;

        public FoodTray(Table table)
        {
            targetTable = table;
            foodList = new List<FoodData>();
        }
        public int GetPrice()
        {
            if (foodList.Count == 0) return 0;
            return foodList.Sum(foodData => foodData.price);
        }
    }
}