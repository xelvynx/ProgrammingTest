using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool canMove = true;
    //Do you create a base player with the ability to move and change their controls within their own separate scripts? P1 vs P2 
    public int speed;
    private int inventoryLimit = 2;
    public List<Vegetable> inventory = new List<Vegetable>();
    private Vector3 movement;
    public void Update()
    {
        if (canMove)
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), transform.position.z);
            transform.position += movement * Time.deltaTime * speed;
        }
    }
    public void MovementControl() 
    {
        canMove = !canMove;
    }
    public void AddToInventory(Vegetable vege)
    {
        if(inventory.Count < inventoryLimit) 
        {
            inventory.Add(vege);
        }
        else { return; }
    }
    public void RemoveVegetable() 
    {
        
        inventory.RemoveAt(0);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Vegetable")
        {
            AddToInventory(other.GetComponent<Vegetable>());
        }
    }

}
