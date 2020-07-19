using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CustomerSlider : MonoBehaviour
{
    public Transform customerLocation;
    private Slider slider;
    private float customerPatience;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        transform.position = Camera.main.WorldToScreenPoint(customerLocation.position) + Vector3.up*50;
    }
    private void Update()
    {
        slider.value -= customerPatience*Time.deltaTime;
    }

}
