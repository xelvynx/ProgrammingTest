using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : MonoBehaviour
{
    public bool inUse = false;
    private float chopTime;
    public VegePlate vegePlate;

    public void Start()
    {
        vegePlate = GetComponent<VegePlate>(); 
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
                    player.RemoveVegetable();
                    player.MovementControl();
                    player.Invoke("MovementControl",chopTime);
                    Invoke("ReverseInUse", chopTime);
                }
            }
        }
    }
    public void ReverseInUse() { inUse = !inUse; }
   
}
