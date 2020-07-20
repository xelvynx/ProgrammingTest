using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public LayerMask checkableLayer;
    private Player player;
    public void Start()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interaction();
        }
    }

    private void Interaction()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, .5f, checkableLayer);
        Debug.Log(collider.name);

        if (collider.tag == "Plate")
        {
            if (player.inventory.Count > 0)
            {
                collider.GetComponent<Vegetable>().GetVegetable(player.inventory[0]);
                player.RemoveVegetable();
            }
        }
        if (collider.tag == "Vegetable")
        {
            VegetableType vtype = collider.GetComponent<Vegetable>().typeOfVegetable;
            Vegetable vege = player.inventory.Find(s => s.typeOfVegetable == vtype);
            player.inventory.Remove(vege);
        }
        if (collider.tag == "CuttingBoard")
        {
            CuttingBoard cb = collider.GetComponent<CuttingBoard>();
            if (cb.vegePlate.vegetablesOnPlate.Count > 0)
            {
                player.AddToVegePlate(cb.vegePlate);
                cb.ClearVegePlate();
            }
        }
        if (collider.tag == "Trash") 
        {
            player.RemoveVegePlate();
        }
    }
}
