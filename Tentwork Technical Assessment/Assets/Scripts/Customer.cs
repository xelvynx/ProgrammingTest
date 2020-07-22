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
    private float secondsPerIngredient = 45;
    private float customerPatience;
    public float currentPatience;
    private int scoreBonus
    {
        get { return 100 * plateRequest.Count; }
    }
    public int correctNumberOfIngredients;
    private float moodMultiplier = 1;
    private SpriteRenderer spriteRenderer;
    #endregion
    #region Unity Methods
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
            CheckResult(scoreBonus);

        }
    }
    #endregion
    #region Methods
    public void AddPlayer(Player player)
    {
        if (!playersWhoInteracted.Contains(player))
            playersWhoInteracted.Add(player);
    }
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
        if (vegePlate.vegetablesOnPlate.Count == plateRequest.Count)
        {
            //Part1 Check if vegePlate has the ingredients
            for (int i = 0; i < plateRequest.Count; i++)
            {
                VegetableType vType = plateRequest[i].typeOfVegetable;
                if (vegePlate.vegetablesOnPlate.Contains(plateRequest[i]))//(vegePlate.vegetablesOnPlate.Find(s => s.typeOfVegetable == vType))
                {
                    vegePlate.vegetablesOnPlate.Remove(vegePlate.vegetablesOnPlate.Find(s => s.typeOfVegetable == vType));
                    correctNumberOfIngredients++;
                }
            }
            //Part2 decides what mood the customer will be in
            if (correctNumberOfIngredients == plateRequest.Count)
            {
                float happinessCheck = currentPatience / customerPatience;
                if (happinessCheck > .70f)
                    ChangeMood(CustomerMood.Happy);
                else
                    ChangeMood(CustomerMood.Satisfied);
                correctNumberOfIngredients = 0;
                CheckResult(player, scoreBonus);
            }
            else
            {
                IncorrectOrder();
            }
        }
        else
        {
            IncorrectOrder();
        }
    }

    private void IncorrectOrder()
    {
        correctNumberOfIngredients = 0;
        ChangeMood(CustomerMood.Angry);
        moodMultiplier = 3;
    }

    public void CheckResult(Player player, int scoreAddition)
    {
        if (mood == CustomerMood.Happy)
        {
            //Spawn Powerup
            PowerupManager.Instance.RequestPowerup(player.gameObject.name);
            GameManager.Instance.PointDistribution(player, scoreAddition);

        }
        if (mood == CustomerMood.Satisfied)
        {
            GameManager.Instance.PointDistribution(player, scoreAddition);
        }
        DeactivateCustomer();
    }
    public void CheckResult(int scoreAddition)
    {

        if (mood == CustomerMood.Angry)
            GameManager.Instance.PointDistribution(playersWhoInteracted.ToArray(), -(scoreAddition * 2));
        if (mood == CustomerMood.Dissatisfied)
            GameManager.Instance.PointDistribution(-scoreAddition);
        DeactivateCustomer();
    }
    private void DeactivateCustomer()
    {
        moodMultiplier = 1;
        GetComponent<CustomerUI>().slider.gameObject.SetActive(false);
        GetComponent<CustomerUI>().text.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        plateRequest.Clear();
        ChangeMood(CustomerMood.Satisfied);
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
    
    #endregion
}
