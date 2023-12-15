using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TableAndChair
{
    public class ChairManager : MyMonobehaviour
    {
        [SerializeField] private List<Chair> chairs = new();

        protected override void LoadComponents()
        {
            base.LoadComponents();
            if(chairs.Count > 0) return;
            if(transform.childCount <= 0) return;
            chairs.Clear();
            foreach (Transform child in transform)
            {
                if (!child.TryGetComponent(out Chair chair)) continue;
                chairs.Add(chair);
            }
        }

        public void SetupChair(Sprite rightChair, Sprite leftChair)
        {
            chairs[0].spriteRenderer.sprite = leftChair;
            chairs[1].spriteRenderer.sprite = rightChair;
        }

        public bool IsEmptyChairs()
        {
            return chairs.All(c => c.IsEmpty());
        }
        
        public Chair GetEmptyChair()
        {
            if(chairs.Count <= 0) return null;
            return chairs.FirstOrDefault(t => t.ChairStatus == ChairStatus.Empty);
        }
        
        public bool AreAllCustomersSeated()
        {
            var tempChairs = chairs.Where(chair => chair.ChairStatus != ChairStatus.Empty).ToList();
            if (tempChairs.Count <= 0) return false;

            return tempChairs.All(c => c.ChairStatus == ChairStatus.Occupied);
        }

        public int GetNumberOfChairsOccupied()
        {
            return chairs.Count(c => c.ChairStatus == ChairStatus.Occupied);
        }

        public List<Chair> GetChairsOccupied()
        {
            return chairs.Where(c => c.ChairStatus == ChairStatus.Occupied).ToList();
        }
    }
}
