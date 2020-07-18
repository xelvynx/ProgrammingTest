using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
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
 * 
 *      Vegetables
 *      
 * Veges can be grabbed from both tables on either side of map
 * Veges take time to chop, chops 1 vegetable at a time - player can't move during chopping
 * Once item is chopped, remove from inventory, put chopped veges on plate, After vegetable is chopped, you can THEN place another vege,
 *      recipe isn't complete until 3 ingredients are in
 * 
 * Combination/salad can be picked up after completed, and can be tossed in trashcan, but results in negative points

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

