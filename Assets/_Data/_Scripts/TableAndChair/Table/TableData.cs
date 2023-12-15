using UnityEngine;

namespace TableAndChair
{
    [CreateAssetMenu(menuName = "SO/Table Data", fileName = "New Table Data")]
    public class TableData : ScriptableObject
    {
        public string tableName;
        public Sprite icon;
        public Sprite tableSprite;
        public Sprite leftChairSprite;
        public Sprite rightChairSprite;
        public int cost;
    }
}