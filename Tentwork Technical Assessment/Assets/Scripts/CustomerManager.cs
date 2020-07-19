using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CustomerManager : MonoBehaviour
{
    
    public Vegetable[] vegetables;
    public GameObject[] customers;
    public Slider[] customerSliders;
    public int vegetableRequestCount = 3;
    public void Start()
    {
        ActivateCustomer();
    }

    public void ActivateCustomer() 
    {
        for (int i = 0; i < customers.Length; i++) 
        {
            if (!customers[i].activeInHierarchy) 
            {
                customerSliders[i].gameObject.SetActive(true);
                customers[i].SetActive(true);
                GeneratePlate(customers[i].GetComponent<Customer>());
                return;
            }
        }
        return;
    }

    void GeneratePlate(Customer customer) 
    {
        for(int i = 0; i < vegetableRequestCount; i++) 
        {
            customer.plateRequest.Add(vegetables[Random.Range(0, vegetables.Length)]);
        }
    }

}
