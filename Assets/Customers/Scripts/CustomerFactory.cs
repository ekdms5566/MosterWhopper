using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Customers.Scripts
{
[Serializable]
public class CustomerFactory
{
    class CustomerNotAssignedException : Exception
    {
        public CustomerNotAssignedException()
        {
        }

        public CustomerNotAssignedException(string message)
            : base(message)
        {
        }
    }

    [SerializeField] private int burgerLevel;
    [SerializeField] private float spawnRate;
    [SerializeField] private float customerWaitingTime;
    [SerializeField] private List<GameObject> customers;
    [SerializeField] private List<int> starScore;
    
    private int currentWave;

    public GameObject Create()
    {
        if(currentWave == customers.Count) return null;

        GameObject instantiated = Object.Instantiate(customers[currentWave++]);
        Customer customer = instantiated.GetComponent<Customer>();
        customer.SetCustomerInfo(new Customer.CustomerInfo(burgerLevel));
        if(customerWaitingTime <= 0.0f) Debug.LogError("Invalid customer waiting time.");
        customer.setWaitingTime(customerWaitingTime);
        return instantiated;
    }

    public int getStarScore(int score)
    {
        if (starScore.Count != 3)
        {
            Debug.LogWarning("starscore threshold size is not 3.");
            return 0;
        }

        int i;
        int result = 0;
        for (i = 0; i < 3; i++)
        {
            if (score < starScore[i]) break;
            result++;
        }

        return result;
    }
    public bool IsWaveOver()
    {
        return currentWave >= customers.Count;
    }

    public void DoReset()
    {
        currentWave = 0;
    }

    public float GetSpawnRate()
    {
        return spawnRate;
    }
}
}