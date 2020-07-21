using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CuttingBoard : MonoBehaviour
{
    public bool inUse = false;
    private float chopTime;
    public VegePlate vegePlate;
    public Text text;
    public void Start()
    {
        vegePlate = GetComponent<VegePlate>();
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!inUse && vegePlate.vegetablesOnPlate.Count<3)
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
                    UIManager.Instance.UpdateText(text, vegePlate.vegetablesOnPlate.ToArray(), vegePlate.vegetablesOnPlate.Count);
                    player.RemoveVegetable();
                    player.controls.MovementControl();
                    player.controls.Invoke("MovementControl", chopTime);
                    Invoke("ReverseInUse", chopTime);
                }
            }
        }
    }
    public void ReverseInUse() { inUse = !inUse; }
    public void ClearVegePlate()
    {
        vegePlate.vegetablesOnPlate.Clear();
        UIManager.Instance.UpdateText(text, null, 0);
    }

}
