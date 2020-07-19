using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Customer : MonoBehaviour
{
    public List<Vegetable> plateRequest = new List<Vegetable>();
    public List<Player> playersWhoInteracted = new List<Player>();
    public CustomerMood mood;
    public Slider slider;
    private float secondsPerIngredient = 100;
    private float customerPatience;
    public float currentPatience;
    private int pointsAwarded;
    public int correctNumberOfIngredients;
    //Dissatisfied Customer waits until currentPatience hits 0.  When 0 hits, both players will lose points
    //Angry Customer - comes from being given incorrect orders. will lose patience a bit faster, person or people who provided incorrect order will lose double points
    //Happy customer - given dish with 70% remaining. Awards player with powerup bonus


    // Start is called before the first frame update
    void Start()
    {
        mood = CustomerMood.Normal;
        customerPatience = plateRequest.Count * secondsPerIngredient;
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position) + Vector3.up * 40;
        currentPatience = customerPatience;
    }
    private void Update()
    {
        currentPatience -= Time.deltaTime;
        slider.value = currentPatience / customerPatience;
    }

    public void CheckPlates(VegePlate vegePlate)
    {
        for(int i = 0; i< plateRequest.Count; i++) 
        {
            VegetableType vType = plateRequest[i].typeOfVegetable;
            Debug.Log("It's a " + vType.ToString());
            if (vegePlate.vegetablesOnPlate.Find(s => s.typeOfVegetable == vType))
            {
                Debug.Log("Success");
                vegePlate.vegetablesOnPlate.Remove(vegePlate.vegetablesOnPlate.Find(s => s.typeOfVegetable == vType));
                correctNumberOfIngredients++;
            }
            ////vegePlate.vegetablesOnPlate.Find(s => s.typeOfVegetable == plateRequest[i].typeOfVegetable)
            //if (vType) 
            //{
            //    //vegePlate
            //    correctNumberOfIngredients++;
            //}
        }
        if(correctNumberOfIngredients>= 3) 
        {
            Debug.Log("Correct # of ing = " + correctNumberOfIngredients);
            correctNumberOfIngredients = 0;
            Debug.Log("Right");
        }
        else { correctNumberOfIngredients = 0; Debug.Log("Wrong"); }
        

        //bool b = false;
        //foreach (Vegetable vege in plateRequest)
        //{
        //    if (vegePlate.vegetablesOnPlate.Contains(vege))
        //    {
        //        b = true;
        //        Debug.Log("yay");
        //    }
        //    else 
        //    {
        //        Debug.Log("Aww Man");
        //        b = false;
        //        return; 
        //    }
        //}
        //if (!b) { Debug.Log("Lose"); }
        //else { Debug.Log("Win"); }
        //float happinessCheck = currentPatience / customerPatience;
        //if (happinessCheck > .70f)
        //{
        //    Debug.Log("SoHappy");
        //    //Spawn Powerup
        //    GivePoints();
        //}
        //else
        //{

        //    GivePoints();
        //}

    }
    public void AddPlayer(Player player)
    {
        if (!playersWhoInteracted.Contains(player))
            playersWhoInteracted.Add(player);
    }
    public void GivePoints()
    {
        foreach(Player player in playersWhoInteracted) 
        {
            player.AddScore(pointsAwarded);
        }
        slider.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
