using ObjectPooling;
using TableAndChair;
using UnityEngine;

namespace Customers
{
    public class CustomerHolder : MonoBehaviour
    {
        [SerializeField] private ObjectPool objectPool;
        public bool test = false;
        public int number = 0;

        private void Awake()
        {
            objectPool = GetComponent<ObjectPool>();
        }

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.F))
            // {
            //     test = true;
            //     number = Random.Range(1, 3);
            // }
            //
            // if (test)
            // {
            //     SetEmptyTableToCustomer();
            // }
        }

        public void Test()
        {
            number = Random.Range(1, 3);
            SetEmptyTableToCustomer();
        }

        private void SetEmptyTableToCustomer()
        {
            Table table = TableManager.Instance.GetEmptyTable();
            if(table == null) return;
            
            for (int i = 0; i < number; i++)
            {
                CustomerBehaviour customer = SpawnCustomer();
                Chair chair = table.ChairManager.GetEmptyChair();
                if(customer == null || chair == null) return;

                customer.targetChair = chair;
                customer.targetTable = table;
                customer.targetTransform = chair.transform;
                table.SetTableStatus(TableStatus.Reserved);
                chair.SetChairStatus(ChairStatus.Reserved);
            }
            
            test = false;
        }
        private CustomerBehaviour SpawnCustomer()
        {
            GameObject obj = objectPool.GetObjectFromPool();
            if(obj == null) return null;
            if (!obj.TryGetComponent(out CustomerBehaviour customer)) return null;
            obj.transform.position = Door.Instance.transform.position + new Vector3(Random.Range(-2f, 2f), 0f, 0f);
            obj.transform.rotation = Quaternion.identity;

            return customer;
        }

        public void ReturnCustomerToPool(GameObject obj)
        {
            objectPool.ReturnObjectToPool(obj);
        }
    }
}
