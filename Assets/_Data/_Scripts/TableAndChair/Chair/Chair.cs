using UnityEngine;

namespace TableAndChair
{
    public enum ChairStatus
    {
        Empty,
        Reserved,
        Occupied
    }
    public class Chair : MonoBehaviour
    {
        [SerializeField] private ChairStatus chairStatus = ChairStatus.Empty;
        public ChairStatus ChairStatus => chairStatus;

        public Transform sitPoint;
        
        public void SetChairStatus(ChairStatus status)
        {
            chairStatus = status;
        }
    }
}
