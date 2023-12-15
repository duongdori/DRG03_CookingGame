using System;
using System.Collections.Generic;
using ObjectPooling;
using TableAndChair;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Customers
{
    public class CustomerHolder : MonoBehaviour
    {
        [SerializeField] private List<CustomerBehaviour> customers = new();
        [SerializeField] private ObjectPool<CustomerBehaviour> objectPool;
        public int number = 0;
        public float timer = 0f;
        public float delay = 0f;

        private void Awake()
        {
            if (objectPool == null)
            {
                Debug.LogWarning(transform.name + ": ObjectPool is null", gameObject);
                objectPool = new ObjectPool<CustomerBehaviour>(customers, 10, transform);
                objectPool.Setup();
            }
        }
        
        [ContextMenu("Create Customer")]
        private void CreateCustomer()
        {
            objectPool = new ObjectPool<CustomerBehaviour>(customers, 10, transform);
            objectPool.Setup();
        }

        private void Update()
        {
            Table table = TableManager.Instance.GetEmptyTable();
            if(table == null) return;

            if (timer > delay)
            {
                timer = 0f;
                delay = Random.Range(2, 5);
                SetEmptyTableToCustomer(table);
            }

            timer += Time.deltaTime;
        }

        private void SetEmptyTableToCustomer(Table table)
        {
            number = Random.Range(1, 3);
            for (int i = 0; i < number; i++)
            {
                CustomerBehaviour customer = SpawnCustomer();
                Chair chair = table.ChairManager.GetEmptyChair();
                if(customer == null || chair == null) return;

                customer.targetChair = chair;
                customer.targetTable = table;
                customer.targetTransform = chair.sitPoint;
                table.SetTableStatus(TableStatus.Reserved);
                chair.SetChairStatus(ChairStatus.Reserved);
            }
        }
        private CustomerBehaviour SpawnCustomer()
        {
            CustomerBehaviour customer = objectPool.GetObjectFromPool();
            if (customer == null) return null;
            
            customer.transform.position = Door.Instance.transform.position + new Vector3(Random.Range(-2f, 2f), 0f, 0f);
            customer.transform.rotation = Quaternion.identity;

            return customer;
        }

        public void ReturnCustomerToPool(CustomerBehaviour obj)
        {
            objectPool.ReturnObjectToPool(obj);
        }
    }
}
