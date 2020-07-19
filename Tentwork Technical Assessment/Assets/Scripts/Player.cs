using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool canMove = true;
    public int score;
    //Do you create a base player with the ability to move and change their controls within their own separate scripts? P1 vs P2 
    public int speed;
    private int inventoryLimit = 2;
    public List<Vegetable> inventory = new List<Vegetable>();
    private VegePlate vegePlate;
    private Vector3 movement;
    public void Start()
    {
        vegePlate = GetComponent<VegePlate>();
    }
    public void Update()
    {
        if (canMove)
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), transform.position.z);
            transform.position += movement * Time.deltaTime * speed;
        }
    }
    public void AddScore(int i) { score += i; }
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
    public void ReturnVegetable() { }
    public void RemoveVegetable() 
    {
        inventory.RemoveAt(0);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Vegetable")
        {
            AddToInventory(collision.GetComponent<Vegetable>());
        }
        if(collision.tag == "Customer") 
        {
            if (inventory.Count > 0)
            {
                VegePlate vege = CheckForPlate();
                if (vege != null)
                {
                    collision.GetComponent<Customer>().CheckPlates(vege,this);
                    inventory.Remove(vege);

                }

            }
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "CuttingBoard") 
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                VegePlate choppingBoardVeges = collision.GetComponent<CuttingBoard>().vegePlate;
                for (int i = 0; i< choppingBoardVeges.vegetablesOnPlate.Count; i++) 
                {
                    vegePlate.vegetablesOnPlate.Add(choppingBoardVeges.vegetablesOnPlate[i]);
                }
                AddToInventory(vegePlate);
                collision.GetComponent<CuttingBoard>().ClearVegePlate();
            }
        }
        if(collision.tag == "Plate") 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (inventory.Count > 0)
                {
                    collision.GetComponent<Vegetable>().GetVegetable(inventory[0]);
                    RemoveVegetable();
                }
            }
        }
        if(collision.tag == "Vegetable") 
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                VegetableType vtype = collision.GetComponent<Vegetable>().typeOfVegetable;
                Vegetable vege = inventory.Find(s => s.typeOfVegetable == vtype);
                inventory.Remove(vege);
            }
        }
    }
    public VegePlate CheckForPlate() 
    {

        foreach(Vegetable vege in inventory) 
        {
            if (vege.GetComponent<VegePlate>())
                return (VegePlate)vege;
        }
        return null;
    }
}
