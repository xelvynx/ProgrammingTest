using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Customer : MonoBehaviour
{
    #region Variables
    public static event OnCustomerLeave CustomerLeave;
    public List<Vegetable> plateRequest = new List<Vegetable>();
    public List<Player> playersWhoInteracted = new List<Player>();
    public CustomerMood mood;
    public Slider slider;
    private float secondsPerIngredient = 100;
    private float customerPatience;
    public float currentPatience;
    private int scoreAddition = 100;
    public int correctNumberOfIngredients;
    private float moodMultiplier = 1;
    
    //Dissatisfied Customer waits until currentPatience hits 0.  When 0 hits, both players will lose points
    //Angry Customer - comes from being given incorrect orders. will lose patience a bit faster, person or people who provided incorrect order will lose double points
    //Happy customer - given dish with 70% remaining. Awards player with powerup bonus

    #endregion
    void Start()
    {
        CustomerLeave += DeactivateCustomer;
        mood = CustomerMood.Satisfied;
        customerPatience = plateRequest.Count * secondsPerIngredient;
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position) + Vector3.up * 40;
        currentPatience = customerPatience;
    }
    private void Update()
    {
        currentPatience -= Time.deltaTime*moodMultiplier;
        slider.value = currentPatience / customerPatience;
        if(currentPatience <= 0)
        {
            if(mood!= CustomerMood.Angry)
            ChangeMood(CustomerMood.Dissatisfied);//Customer mood changed to dissatisfied
        }
    }
    #region Methods
    public void CheckPlates(VegePlate vegePlate,Player player)
    {
        AddPlayer(player);
        //Part1 Check if vegePlate has the ingredients
        for(int i = 0; i< plateRequest.Count; i++) 
        {
            VegetableType vType = plateRequest[i].typeOfVegetable;
            if (vegePlate.vegetablesOnPlate.Find(s => s.typeOfVegetable == vType))
            {
                vegePlate.vegetablesOnPlate.Remove(vegePlate.vegetablesOnPlate.Find(s => s.typeOfVegetable == vType));
                correctNumberOfIngredients++;
            }
        }

        //Part2 decides what mood the customer will be in
        if(correctNumberOfIngredients>= 3) 
        {
            float happinessCheck = currentPatience / customerPatience;
            if (happinessCheck > .70f) // Checks to see if 
            {
                Debug.Log("SoHappy");
                ChangeMood(CustomerMood.Happy);//Customer mood changed to happy//Spawn Powerup
            }
            else 
                ChangeMood(CustomerMood.Satisfied);//Customer mood changed to satisfied
            correctNumberOfIngredients = 0;
            Debug.Log("Right");
            CheckResult(player, scoreAddition);
            DeactivateCustomer();//static event

        }
        else 
        { 
            correctNumberOfIngredients = 0;
            ChangeMood(CustomerMood.Angry);//Customer mood changed to angry
            Debug.Log("Wrong"); 
        }
    }
    public void CheckResult(Player player,int scoreAddition) 
    {
        switch (mood)
        {
            case CustomerMood.Happy:
                //Spawn Powerup
                PointDistribution(player, scoreAddition);
                break;
            case CustomerMood.Satisfied:
                PointDistribution(player, scoreAddition);
                break;
            case CustomerMood.Angry:
                PointDistribution(playersWhoInteracted.ToArray(), -(scoreAddition * 2));
                break;
            case CustomerMood.Dissatisfied:
                PointDistribution(GameManager.Instance.player1.GetComponent<Player>(), -scoreAddition);
                PointDistribution(GameManager.Instance.player2.GetComponent<Player>(), -scoreAddition);
                break;
        }

    }
    private void DeactivateCustomer()
    {
        this.slider.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
    public void ChangeMood(CustomerMood customerMood) 
    {
        mood = customerMood; 
    }
    public void AddPlayer(Player player)
    {
        if (!playersWhoInteracted.Contains(player))
            playersWhoInteracted.Add(player);
    }
    public void PointDistribution(Player players, int score) { }
    public void PointDistribution(Player[] players, int score) { }
    #endregion
}
