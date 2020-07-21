using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public Text text;
    private int score;
    public int Score { get { return score; }  set { score = value; } }
    private int inventoryLimit = 2;
    public List<Vegetable> inventory = new List<Vegetable>();
    private VegePlate vegePlate;
    public Controls controls;

    public void Start()
    {
        controls = GetComponent<Controls>();
        vegePlate = GetComponent<VegePlate>();
    }

    public void AddScore(int i) { score += i; }
    public int GetScore() { return score; }

    public void AddToInventory(Vegetable vege)
    {
        if(inventory.Count < inventoryLimit) 
        {
            inventory.Add(vege);
            UIManager.Instance.UpdateText(text, inventory.ToArray(), inventory.Count);
        }
        else { return; }
    }
    public void RemoveVegetable() 
    {
        inventory.RemoveAt(0);
        UIManager.Instance.UpdateText(text, inventory.ToArray(), inventory.Count);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Vegetable")
        {   
            AddToInventory(collision.GetComponent<Vegetable>());
        }
        if(collision.tag == "Customer") 
        {
            if (inventory.Count > 0)
            {
                VegePlate vege = CheckForPlate();//returns vege plate 
                if (vege != null)
                {
                    collision.GetComponent<Customer>().CheckPlates(vege,this);
                    inventory.Remove(vege);//removes vege plate
                    vegePlate.vegetablesOnPlate.Clear();
                    UIManager.Instance.UpdateText(text, inventory.ToArray(), inventory.Count);
                }

            }
        }
    }
    public void AddToVegePlate(VegePlate choppingBoardPlate) 
    {
        for (int i = 0; i < choppingBoardPlate.vegetablesOnPlate.Count; i++)
        {
            vegePlate.vegetablesOnPlate.Add(choppingBoardPlate.vegetablesOnPlate[i]);
        }
        AddToInventory(vegePlate);
    }
    public VegePlate CheckForPlate() 
    {
        foreach(Vegetable vege in inventory) 
        {
            if (vege.GetComponent<VegePlate>())
            {
                Debug.Log("Returned a vege plate");
                return (VegePlate)vege;
            }
        }
        Debug.Log("Returned nothing");
        return null;
    }

    public void RemoveVegePlate() 
    {
        VegePlate vegPlate = CheckForPlate();
        inventory.Remove(vegPlate);
        vegePlate.vegetablesOnPlate.Clear();
        UIManager.Instance.UpdateText(text, inventory.ToArray(), inventory.Count);
    }
}
