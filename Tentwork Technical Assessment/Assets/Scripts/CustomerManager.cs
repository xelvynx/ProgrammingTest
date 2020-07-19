using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CustomerManager : MonoBehaviour
{
    
    public Vegetable[] vegetables;
    public GameObject[] customers;
    public Slider[] customerSliders;
    public Text[] customerText;
    public int vegetableRequestCount = 3;
    public float delayBeforeCustomerSpawn = 5;
    public int activeCustomers = 0;
    private bool coroutineRunning;
    public void Start()
    {
        Customer.CustomerLeave += DecrementCustomerAmount;
        StartCoroutine("GenerateCustomer");
    }

    public void ActivateCustomer() 
    {
        for (int i = 0; i < customers.Length; i++) 
        {
            if (!customers[i].activeInHierarchy) 
            {
                customerSliders[i].gameObject.SetActive(true);
                customerText[i].gameObject.SetActive(true);
                customers[i].SetActive(true);
                
                GeneratePlate(customers[i].GetComponent<Customer>());
                activeCustomers++;
                return;
            }
        }
        coroutineRunning = false; //When it activates all the customers, it will switch off which means coroutine is not running anymore
        return;
    }

    void GeneratePlate(Customer customer) 
    {
        vegetableRequestCount = Random.Range(1, 4);
        
        for(int i = 0; i < vegetableRequestCount; i++) 
        {
            customer.plateRequest.Add(vegetables[Random.Range(0, vegetables.Length)]);
        }
        customer.GetComponent<CustomerUI>().AddText(customer.plateRequest.Count);
    }
    IEnumerator GenerateCustomer() 
    {
        while (activeCustomers < 5)
        {
            coroutineRunning = true;
            ActivateCustomer();
            yield return new WaitForSeconds(delayBeforeCustomerSpawn);
        }
    }
    public void DecrementCustomerAmount() 
    { 
        activeCustomers--;
        if (!coroutineRunning) StartCoroutine("GenerateCustomer");
    }

}
