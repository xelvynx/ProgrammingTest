using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    #region Variables
    public Sprite speedSprite;
    public Sprite timeSprite;
    public Sprite scoreSprite;
    public PowerUpType pType;
    public string playerPickup;
    private SpriteRenderer spriteRenderer;
    #endregion
    #region Unity Methods
    public void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == playerPickup)
        {
            Player p = collision.GetComponent<Player>();
            switch (pType)
            {
                case PowerUpType.Speed:
                    //implement speed
                    p.controls.StartCoroutine("SpeedBonus", 1.5f);
                    SetInactive();
                    break;
                case PowerUpType.Time:
                    //Add Time to game manager
                    GameManager.Instance.GiveTime(p, 10);
                    SetInactive();
                    break;
                case PowerUpType.Score:
                    //Add score
                    GameManager.Instance.PointDistribution(p, 100);
                    SetInactive();
                    break;
                default:
                    break;
            }
        }
        else { return; }
    }
    #endregion
    #region Methods
    public void SpawnPowerUp(PowerUpType type, string player)
    {
        playerPickup = player;
        switch (type)
        {
            case PowerUpType.Speed:
                pType = type;
                spriteRenderer.sprite = speedSprite;
                break;
            case PowerUpType.Time:
                pType = type;
                spriteRenderer.sprite = timeSprite;
                break;
            case PowerUpType.Score:
                pType = type;
                spriteRenderer.sprite = scoreSprite;
                break;
            default:
                break;
        }
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }
    #endregion
}
