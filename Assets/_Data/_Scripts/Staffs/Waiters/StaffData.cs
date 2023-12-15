using UnityEngine;

[CreateAssetMenu(menuName = "SO/Staff Data", fileName = "New Staff Data")]
public class StaffData : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public int cost;
}
