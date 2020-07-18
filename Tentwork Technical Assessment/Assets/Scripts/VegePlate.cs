using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegePlate : MonoBehaviour
{
    public List<Vegetable> vegetablesOnPlate = new List<Vegetable>();

    public void AddToPlate(Vegetable vege) 
    {
        vegetablesOnPlate.Add(vege);

    }
}
