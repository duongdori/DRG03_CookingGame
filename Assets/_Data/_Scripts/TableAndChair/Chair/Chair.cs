using UnityEngine;

namespace TableAndChair
{
    public enum ChairStatus
    {
        Empty,
        Reserved,
        Occupied
    }
    public class Chair : MyMonobehaviour
    {
        [SerializeField] private ChairStatus chairStatus = ChairStatus.Empty;
        public ChairStatus ChairStatus => chairStatus;

        public SpriteRenderer spriteRenderer;

        public Transform sitPoint;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            if(spriteRenderer != null) return;
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void SetChairStatus(ChairStatus status)
        {
            chairStatus = status;
        }

        public bool IsEmpty()
        {
            return chairStatus == ChairStatus.Empty;
        }
    }
}
