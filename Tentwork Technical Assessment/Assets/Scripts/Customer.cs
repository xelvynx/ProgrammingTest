using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Customer : MonoBehaviour
{
    public List<Vegetable> plateRequest = new List<Vegetable>();
    public Transform customerLocation;
    private Slider slider;
    private float customerPatience;
    // Start is called before the first frame update
    void Start()
    {


        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position) + Vector3.up * 50;
    }
    private void Update()
    {
        slider.value -= customerPatience * Time.deltaTime;
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
