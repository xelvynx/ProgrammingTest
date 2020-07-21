using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event GameOver GameIsOver;
    public GameObject player1GO;
    public GameObject player2GO;
    private float startingTime = 60;
    private float player1Time;
    private float player2Time;
    private int player1Score;
    private int player2Score;
    private GameState state = GameState.Running;
    private Player player1;
    private Player player2;
    private Controls player1Controls;
    private Controls player2Controls;
    //Things to add
    //POwerup spawning within a certain area- not yet started
    //Distinguishing cutting boards for player who used first - not yet started
    //Top 10 scoreboard - not yet started
    //Reset option on endgame screen - not yet started
    //Fix customer slider + text location for adjustable - not yet started
    //Change player score based off throwing away in trashcan - not yet started
    //consolidated code - in progress


    //add flourishes including changing cutting board color of the player who used it.  player opacity changes from clear to solid color when cutting on board
    //turn customer red if angry and reset slider bar
    //Resolved - Issue currently happening with on reactivate, customer quickly disappears due to currentpatience being reduced too quickly and ends up going negative, current patience not resetting when reactivated
    //Answer - moved the NewSpawn(resets timer and slider ratio back to 1) to after plate is generated
    //if player gives combination more than what they requested, make it wrong - not yet started
    //if player runs to chopping board when 3 ingredients are already on there, pickup? do nothing? - not yet started

    //Completed
    //Text displaying character inventory - Done!
    //individual controls - Done
    //Timer - Done
    //Game finishes when both timers hit 0 - not yet started
    //using trash can removes ingredients in vege plate
    public void Start()
    {
        player1Time = startingTime;
        player2Time = startingTime;
        player1 = player1GO.GetComponent<Player>();
        player2 = player2GO.GetComponent<Player>();
        player1Controls = player1GO.GetComponent<Controls>();
        player2Controls = player1GO.GetComponent<Controls>();

    }

    public void Update()
    {
        UpdateTimers();
    }
    public void EndGame()
    {
        state = GameState.Ended;
        Time.timeScale = 0;
        player1Controls.EndGameMovement();
        player2Controls.EndGameMovement();
        GameIsOver(Winner());
    }
    public Player Winner() 
    {
        if(player1Score > player2Score) 
        {
            return player1;
        }
        else { return player2; }


    }
    public void UpdateTimers()
    {
        player1Time -= Time.deltaTime;
        player2Time -= Time.deltaTime;
        UIManager.Instance.UpdatePlayer1Timer(player1Time);
        UIManager.Instance.UpdatePlayer2Timer(player2Time);
        if (player1Time <= 0 && player2Time <= 0 && state == GameState.Running)
        {
            EndGame();
        }
    }
    public void PointDistribution(Player player, int score)
    {
        player.Score += score;
        if (player.name == "Player1")
        {
            player1Score = player1.GetScore();
            UIManager.Instance.UpdatePlayer1Score(player1Score);
        }
        else
        {
            player2Score = player2.GetScore();
            UIManager.Instance.UpdatePlayer2Score(player2Score);
        }
    }
    public void PointDistribution(int score)
    {
        PointDistribution(player1, score);
        PointDistribution(player2, score);
    }

    public void PointDistribution(Player[] players, int score)
    {
        foreach (Player player in players)
        {
            PointDistribution(player, score);
        }
    }
}
/*
 * Things to Add
 * Static top down view
 * Two different set of controls per player
 * Distinguishable Chars
 * 
 *      Players
 * Players can hold 2 items at a time
 * First in first out for vegetables onto the chopping board
 * Distinguish different controls for UP, DOWN, LEFT, RIGHT, ACTION{action will either pickup or discard based off of actions available)
 * 
 *      Vegetables
 * Picked veges can be placed back on cutting board
 * Show on UI what Items they are carrying. Inventory slot 1 = Vege1, Inventory slot 2 = Vege2
 * Veges can be grabbed from both tables on either side of map
 * Veges take time to chop, chops 1 vegetable at a time - player can't move during chopping
 * Once item is chopped, remove from inventory, put chopped veges on plate, After vegetable is chopped, you can THEN place another vege,
 *      recipe isn't complete until 3 ingredients are in
 * 
 * Combination/salad can be picked up after completed, and can be tossed in trashcan, but results in negative points

 *      Chopping Board
 *  Chopping board is where you go to get vegetables chopped
 *  Once combination is deemed "Complete" then you can pick it up 
 * 
 * 
 * Customer Interaction
 * Customers can request 1-3 ingredients
 * Correct combination awards player with bonus points
 * Customers have duration before leaving dissatisfied
 *      When duration runs out for dissatisfied customers, both players lose exp
 * Duration that they are willing to wait is based off X number of ingredients requested
 * Incorrect combinations make angry customers
 *      Angry customers lose waiting time faster
 *      if not given correct combination before timer, player who served incorrect order will lose double points
 *      if both players gave incorrectly, both will lose
 *      
 *  PowerUP
 *  If you give a customer their vegeplate before 70% of timer is depleted, you're awarded with random powerup
 *      random powerup will be placed on the map and only the player that activated can loot
 *      
 *  Game Ends when both timers are 0
 *  Highscore list with top 10
 *  
 *  Winning message displayed "Player 1 Wins"
 *  
 *  Reset Options at the end of the game, start another round
 *  
 *  Chopping board, change color when player uses it.
 *  
 */

