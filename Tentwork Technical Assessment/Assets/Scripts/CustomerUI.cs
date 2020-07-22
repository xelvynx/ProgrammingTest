using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CustomerUI : MonoBehaviour
{
    #region Variables
    private Customer customer;
    public Slider slider;
    public Text text;
    #endregion
    #region Unity Methods
    public void Awake()
    {
        customer = GetComponent<Customer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position) + Vector3.up * 30;
        text.transform.position = Camera.main.WorldToScreenPoint(transform.position) + Vector3.down * 45;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = customer.PatienceRatio();
    }
    #endregion
    #region Methods
    public void AddText(int i)
    {
        switch (i)
        {
            case 1:
                text.text = customer.plateRequest[0].typeOfVegetable.ToString();
                break;
            case 2:
                text.text = customer.plateRequest[0].typeOfVegetable.ToString() + ", " + customer.plateRequest[1].typeOfVegetable.ToString();
                break;
            case 3:
                text.text = customer.plateRequest[0].typeOfVegetable.ToString() + ", " + customer.plateRequest[1].typeOfVegetable.ToString() + ", " + customer.plateRequest[2].typeOfVegetable.ToString();
                break;
            default:
                break;
        }
    }
    #endregion
}
