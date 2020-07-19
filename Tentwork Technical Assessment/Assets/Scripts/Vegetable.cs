using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Vegetable : MonoBehaviour
{
    public float cuttingDuration { get; private set; }
    public VegetableType typeOfVegetable;
    public Text nameText;
    void Start()
    {
        ChangeVegetable();
    }

    public void GetVegetable(Vegetable vege) 
    {
        typeOfVegetable = vege.typeOfVegetable;
        ChangeVegetable();
    }
    public void ChangeVegetable() 
    {
        if (typeOfVegetable != VegetableType.Combo)
        {
            gameObject.name = typeOfVegetable.ToString();
            nameText.text = typeOfVegetable.ToString();
        }
        switch (typeOfVegetable) 
        {
            case VegetableType.Cucumber:
                cuttingDuration = 1;
                break;
            case VegetableType.Onion:
                cuttingDuration = 1.5f;
                break;
            case VegetableType.Tomato:
                cuttingDuration = 2;
                break;
            case VegetableType.Raddish:
                cuttingDuration = 2.5f;
                break;
            case VegetableType.Lettuce:
                cuttingDuration = .75f;
                break;
            case VegetableType.Spinach:
                cuttingDuration = .5f;
                break;
            case VegetableType.None:
                cuttingDuration = 0;
                nameText.text = "";
                break;
            default:
                break;
        }

    }
}
