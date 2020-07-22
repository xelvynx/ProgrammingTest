using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    #region Variables
    public LayerMask checkableLayer;
    private Player player;
    private bool CanMove = true;
    private int speed = 5;
    private int currentSpeed;
    private Vector3 movement;

    private float moveX;
    private float moveY;
    private Vector3 move;

    //Made these public to allow for adjustments
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode action;
    #endregion

    #region Unity Methods
    public void Start()
    {
        currentSpeed = speed;
        player = GetComponent<Player>();
        ControlsSetup();
    }
    private void Update()
    {
        if (CanMove)
        {
            Movement();
            if (Input.GetKeyDown(action))
            {
                Interaction();
            }
        }
    }
    #endregion
    #region Methods
    public IEnumerator SpeedBonus(int i)
    {
        currentSpeed += i;
        yield return new WaitForSeconds(3);
        currentSpeed = speed;
    }
    public void ControlsSetup()
    {
        if (gameObject.name == "Player1")
        {
            up = KeyCode.W;
            down = KeyCode.S;
            left = KeyCode.A;
            right = KeyCode.D;
            action = KeyCode.E;
        }
        if (gameObject.name == "Player2")
        {
            up = KeyCode.Keypad8;
            down = KeyCode.Keypad5;
            left = KeyCode.Keypad4;
            right = KeyCode.Keypad6;
            action = KeyCode.Keypad7;
        }
    }
    public void Movement()
    {
        move = new Vector3(Mathf.Clamp(MoveHorizontal(), -1, 1), Mathf.Clamp(MoveVertical(), -1, 1), 0);
        transform.position += move * Time.deltaTime * currentSpeed;
    }
    public float MoveVertical()
    {
        if (Input.GetKey(down))
            return moveY -= .05f;
        if (Input.GetKey(up))
            return moveY += .05f;
        else
            return Mathf.SmoothDamp(moveY, 0, ref moveY, 1);
    }
    public float MoveHorizontal()
    {
        if (Input.GetKey(left))
            return moveX -= .05f;
        if (Input.GetKey(right))
            return moveX += .05f;
        else
            return Mathf.SmoothDamp(moveX, 0, ref moveX, 1);

    }
    public void MovementControl()
    {
        CanMove = !CanMove;
        moveX = 0;
        moveY = 0;
    }
    public void EndGameMovement() { CanMove = false; }
    private void Interaction()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, .5f, checkableLayer);
        if (collider != null)
        {
            string s = collider.tag;
            switch (s) 
            {
                case "Plate":
                    if (player.inventory.Count > 0 && player.inventory[0].typeOfVegetable != VegetableType.Combo)
                    {
                        collider.GetComponent<Vegetable>().GetVegetable(player.inventory[0]);
                        player.RemoveVegetable();
                    }
                    break;
                case "Vegetable":
                    VegetableType vtype = collider.GetComponent<Vegetable>().typeOfVegetable;
                    Vegetable vege = player.inventory.Find(v => v.typeOfVegetable == vtype);
                    player.inventory.Remove(vege);
                    break;
                case "CuttingBoard":
                    CuttingBoard cb = collider.GetComponent<CuttingBoard>();
                    if (cb.vegePlate.vegetablesOnPlate.Count != 0)
                    {
                        player.AddToVegePlate(cb.vegePlate);
                        cb.ClearVegePlate();
                    }
                    break;
                case "Trash":
                    player.RemoveVegePlate();
                    GameManager.Instance.PointDistribution(player, -50);
                    break;
                default:
                    break;
            }
        }
    }
    #endregion
}
