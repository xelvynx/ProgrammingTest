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
    private float secondsPerIngredient = 100;
    private float customerPatience;
    public float currentPatience;
    private int scoreAddition = 100;
    public int correctNumberOfIngredients;
    private float moodMultiplier = 1;

    private SpriteRenderer spriteRenderer;
    //Dissatisfied Customer waits until currentPatience hits 0.  When 0 hits, both players will lose points
    //Angry Customer - comes from being given incorrect orders. will lose patience a bit faster, person or people who provided incorrect order will lose double points
    //Happy customer - given dish with 70% remaining. Awards player with powerup bonus

    #endregion
    private void Awake()
    {
        NewSpawn();
    }
    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        customerPatience = plateRequest.Count * secondsPerIngredient;
        currentPatience = customerPatience;                         
    }
    private void LateUpdate()
    {
        currentPatience -= Time.deltaTime * moodMultiplier;
        PatienceRatio();
        if (currentPatience <= 0)
        {
            if (mood != CustomerMood.Angry)
                ChangeMood(CustomerMood.Dissatisfied);//Customer mood changed to dissatisfied
            CheckResult(scoreAddition);

        }
    }
    #region Methods
    public void NewSpawn() 
    {
        
        customerPatience = plateRequest.Count * secondsPerIngredient;
        currentPatience = customerPatience;
    }
    public float PatienceRatio()
    {
        return currentPatience / customerPatience;
    }
    public void CheckPlates(VegePlate vegePlate, Player player)
    {
        AddPlayer(player);
        //Part1 Check if vegePlate has the ingredients
        for (int i = 0; i < plateRequest.Count; i++)
        {
            VegetableType vType = plateRequest[i].typeOfVegetable;
            if (vegePlate.vegetablesOnPlate.Find(s => s.typeOfVegetable == vType))
            {
                vegePlate.vegetablesOnPlate.Remove(vegePlate.vegetablesOnPlate.Find(s => s.typeOfVegetable == vType));
                correctNumberOfIngredients++;
            }
        }

        //Part2 decides what mood the customer will be in
        if (correctNumberOfIngredients >= plateRequest.Count)
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
            CheckResult(player, scoreAddition);
        }
        else
        {
            correctNumberOfIngredients = 0;
            ChangeMood(CustomerMood.Angry);//Customer mood changed to angry
            //vegePlate.vegetablesOnPlate.Clear();
            moodMultiplier = 1.2f;
            Debug.Log("Wrong");
        }
    }
    public void CheckResult(Player player, int scoreAddition)
    {
        DeactivateCustomer();
        if (mood == CustomerMood.Happy)
        {
            //Spawn Powerup
            Debug.Log("Customer happy");
            GameManager.Instance.PointDistribution(player, scoreAddition);

        }
        if (mood == CustomerMood.Satisfied)
        {
            GameManager.Instance.PointDistribution(player, scoreAddition);
            Debug.Log("Customer satis");
        }
    }
    public void CheckResult(int scoreAddition)
    {
        DeactivateCustomer();
        if (mood == CustomerMood.Angry)
            GameManager.Instance.PointDistribution(playersWhoInteracted.ToArray(), -(scoreAddition * 2));
        if (mood == CustomerMood.Dissatisfied)
            GameManager.Instance.PointDistribution(-scoreAddition);
    }
    private void DeactivateCustomer()
    {
        
        GetComponent<CustomerUI>().slider.gameObject.SetActive(false);
        GetComponent<CustomerUI>().text.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        CustomerLeave();
    }
    public void ChangeMood(CustomerMood customerMood)
    {
        mood = customerMood;
        if(mood == CustomerMood.Angry) 
        {
            spriteRenderer.color = Color.red;
        }
        if(mood == CustomerMood.Satisfied) 
        {
            spriteRenderer.color = Color.white;
        }
    }
    public void AddPlayer(Player player)
    {
        if (!playersWhoInteracted.Contains(player))
            playersWhoInteracted.Add(player);
    }
    private void OnDisable()
    {
        plateRequest.Clear();
        ChangeMood(CustomerMood.Satisfied);
    }
    #endregion
}
