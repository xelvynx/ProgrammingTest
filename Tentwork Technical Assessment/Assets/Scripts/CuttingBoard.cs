using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : MonoBehaviour
{
    public bool inUse = false;
    public VegePlate vegePlate;
    private float choppingTime;
    public void Update()
    {
        if(choppingTime <= 0) 
        {

        }
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!inUse)
        {
            if (collider.tag == "Player")
            {
                inUse = true;

               // collider.GetComponent<Player>().inventory[0]
            }
        }
    }
}
