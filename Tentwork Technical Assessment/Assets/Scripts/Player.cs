﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int inventoryLimit = 2;
    public List<Vegetable> inventory = new List<Vegetable>();

    public void Start()
    {
        
    }

    public void AddVegetable(Vegetable vege) 
    {
        if(inventory.Count < inventoryLimit) 
        {
            inventory.Add(vege);
        }
        else { return; }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Vegetable") 
        {
            AddVegetable(other.GetComponent<Vegetable>());
        }
    }

}