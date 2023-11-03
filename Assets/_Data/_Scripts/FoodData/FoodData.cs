using UnityEngine;

[CreateAssetMenu(menuName = "SO/Food Data", fileName = "New Food Data")]
public class FoodData : ScriptableObject
{
    public int id;
    public new string name;
    public Sprite sprite;
    public GameObject prefab;
    public float preparationTime;
    public int price;
}
