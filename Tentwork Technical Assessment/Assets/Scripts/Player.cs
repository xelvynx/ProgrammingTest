using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    #region Variables
    public int Score { get; private set; }
    public Text text;
    public List<Vegetable> inventory { get; private set; }
    public Controls controls { get; private set; }

    private int inventoryLimit = 2;
    private VegePlate vegePlate;

    #endregion
    #region Unity Methods
    public void Start()
    {
        inventory = new List<Vegetable>();
        controls = GetComponent<Controls>();
        vegePlate = GetComponent<VegePlate>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Vegetable")
        {
            AddToInventory(collision.GetComponent<Vegetable>());
        }
        if (collision.tag == "Customer")
        {
            if (inventory.Count > 0)
            {
                VegePlate vege = CheckForPlate();//returns vege plate 
                if (vege != null)
                {
                    collision.GetComponent<Customer>().CheckPlates(vege, this);
                    inventory.Remove(vege);//removes vege plate
                    vegePlate.vegetablesOnPlate.Clear();
                    UIManager.Instance.UpdateText(text, inventory.ToArray(), inventory.Count);
                }

            }
        }
    }

    #endregion



    public void AddScore(int i) { Score += i; }
    public int GetScore() { return Score; }

    public void AddToInventory(Vegetable vege)
    {
        if (inventory.Count < inventoryLimit)
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
        foreach (Vegetable vege in inventory)
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
