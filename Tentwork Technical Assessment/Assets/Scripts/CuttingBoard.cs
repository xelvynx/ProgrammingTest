using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CuttingBoard : MonoBehaviour
{
    public bool inUse = false;
    private float chopTime;
    public VegePlate vegePlate;
    public Text veggieText;
    public void Start()
    {
        vegePlate = GetComponent<VegePlate>();
        veggieText = GetComponentInChildren<Text>();
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!inUse)
        {
            if (collider.tag == "Player")
            {
                Debug.Log("collision");
                Player player = collider.GetComponent<Player>();
                if (player.inventory.Count > 0)
                {
                    inUse = true;
                    chopTime = player.inventory[0].cuttingDuration;
                    vegePlate.AddToPlate(player.inventory[0]);
                    AddText();
                    player.RemoveVegetable();
                    player.MovementControl();
                    player.Invoke("MovementControl", chopTime);
                    Invoke("ReverseInUse", chopTime);
                }
            }
        }
    }
    public void ReverseInUse() { inUse = !inUse; }
    public void ClearVegePlate()
    {
        vegePlate.vegetablesOnPlate.Clear();
    }
    public void AddText()
    {
        int i = vegePlate.vegetablesOnPlate.Count;
        switch (i)
        {
            case 1:
                veggieText.text = vegePlate.vegetablesOnPlate[0].typeOfVegetable.ToString();
                return;
            case 2:
                veggieText.text = vegePlate.vegetablesOnPlate[0].typeOfVegetable.ToString() + ", " + vegePlate.vegetablesOnPlate[1].typeOfVegetable.ToString();
                return;
            case 3:
                veggieText.text = vegePlate.vegetablesOnPlate[0].typeOfVegetable.ToString() + ", " + vegePlate.vegetablesOnPlate[1].typeOfVegetable.ToString() + ", " + vegePlate.vegetablesOnPlate[2].typeOfVegetable.ToString();
                return;
        }
    }

}
