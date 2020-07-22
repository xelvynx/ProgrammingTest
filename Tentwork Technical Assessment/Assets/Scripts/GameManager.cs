using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Variables
    public static event GameOver GameIsOver;
    public GameObject player1GO;
    public GameObject player2GO;
    private float startingTime = 5;
    private float player1Time;
    private float player2Time;
    private int player1Score;
    private int player2Score;
    private GameState state = GameState.Running;
    private Player player1;
    private Player player2;
    private Controls player1Controls;
    private Controls player2Controls;
    #endregion
    #region Unity Methods
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
  
    #endregion
    #region Methods

    public Player Winner()
    {
        if (player1Score > player2Score)
        {
            return player1;
        }
        else { return player2; }
    }
    #region Time
    public void UpdateTimers()
    {
        if (state == GameState.Running)
        {
            player1Time -= Time.deltaTime;
            player2Time -= Time.deltaTime;
            UIManager.Instance.UpdatePlayer1Timer(player1Time);
            UIManager.Instance.UpdatePlayer2Timer(player2Time);
        }
        if (player1Time <= 0 && player2Time <= 0 && state == GameState.Running)
        {
            EndGame();
        }
    }
    public void GiveTime(Player player, int i)
    {
        if (player.name == "Player1")
        {
            player1Time += i;
        }
        else player2Time += i;
    }
    #endregion
    #region Point Distribution
    public void PointDistribution(Player player, int score)
    {
        player.AddScore(score);
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
    public void EndGame()
    {
        state = GameState.Ended;
       
        player1Controls.EndGameMovement();
        player2Controls.EndGameMovement();
        GameIsOver(Winner());
    }
    #endregion
    #endregion
}
