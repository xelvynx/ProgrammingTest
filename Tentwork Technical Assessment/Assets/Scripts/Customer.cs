using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public List<Vegetable> plateRequest = new List<Vegetable>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckPlates(VegePlate vegePlate) 
    {
        foreach(Vegetable vege in plateRequest) 
        {
            if (vegePlate.vegetablesOnPlate.Contains(vege)) 
            {
                Debug.Log("yay");
            }
            else { Debug.Log("Aww Man"); return; }
        }
        
    }
}
