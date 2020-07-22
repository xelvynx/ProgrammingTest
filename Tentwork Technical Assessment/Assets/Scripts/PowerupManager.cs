using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : Singleton<PowerupManager>
{
    #region Variables
    public List<GameObject> powerUpList = new List<GameObject>();
    public GameObject powerUpPrefab;
    public Transform powerupContainer;
    private float xPos;
    private float yPos;
    private Vector3 randomPos;
    private PowerUpType pType;
    #endregion

    #region Unity Methods
 void Start()
    {
        GeneratePowerup(10);
    }
    #endregion
    #region Methods
    List<GameObject> GeneratePowerup(int amountOfItems)
    {
        for (int i = 0; i < amountOfItems; i++)
        {
            GameObject item = Instantiate(powerUpPrefab, powerupContainer);
            powerUpList.Add(item);
            item.SetActive(false);
        }
        return null;
    }
    public GameObject RequestPowerup(string player)
    {
        xPos = Random.Range(-6.5f, 6.5f);
        yPos = Random.Range(-2.5f, 1.5f);
        randomPos = new Vector3(xPos, yPos);
        pType = (PowerUpType)Random.Range(0, 3);
        //pType = (PowerUpType)Random.Range(0,)
        foreach (GameObject item in powerUpList)
        {
            if (!item.activeInHierarchy)
            {
                Debug.Log("Spawning Powerup");
                //item is Available and will spawn a random powerup

                item.transform.position = randomPos;
                item.SetActive(true);
                item.GetComponent<PowerUp>().SpawnPowerUp(pType, player);
                return item;
            }
        }

        GameObject newItem = Instantiate(powerUpPrefab, powerupContainer);
        newItem.transform.position = randomPos;
        powerUpList.Add(newItem);
        return newItem;

        //need to create new item
        //if we made it to this pooint, we need to generate more items
        //when an item is picked up, setactive to false

    }
    #endregion
}
