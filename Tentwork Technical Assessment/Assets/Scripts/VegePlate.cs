using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class VegePlate : Vegetable
{
    public List<Vegetable> vegetablesOnPlate = new List<Vegetable>();

    public void Start()
    {
        typeOfVegetable = VegetableType.Combo;
    }

    public void AddToPlate(Vegetable vege) 
    {
        if (vegetablesOnPlate.Count < 3)
        {
            vegetablesOnPlate.Add(vege);
        }
        else { return; }
    }
   
}
