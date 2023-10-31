using System.Collections.Generic;
using System.Linq;
using TableAndChair;
using UnityEngine;

namespace Kitchen
{
    public class FoodTray : MonoBehaviour
    {
        public Table targetTable;
        public List<FoodData> foodList = new();

        public int GetPrice()
        {
            if (foodList.Count == 0) return 0;
            return foodList.Sum(foodData => foodData.price);
        }
    }
}