using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : Singleton<UIManager>
{
    #region Fields
    public Text player1ScoreText;
    public Text player1TimerText;
    public Text player2ScoreText;
    public Text player2TimerText;
    public Text winningPlayer;

    public GameObject endGameScreen;
    public HighScoreUI[] highScoreObjects;
    public HighScoreList highScoreList;
    #endregion
    #region Unity Methods
    public void Start()
    {
        GameManager.GameIsOver += EndGame;
    }
    #endregion
    #region Methods
    public void EndGame(Player player) 
    {
        Debug.Log("Part2");
        StartCoroutine(DisplayEndGameScreen(player));
    }
    public IEnumerator DisplayEndGameScreen(Player player) 
    {
        winningPlayer.gameObject.SetActive(true);
        winningPlayer.text = player.name + " Wins";
        yield return new WaitForSeconds(2);
        Debug.Log("Part3");
        winningPlayer.gameObject.SetActive(false);
        endGameScreen.SetActive(true);
        highScoreList.AddTolist(player);//Sets highscore
        SetHighScores();
    }
    public void SetHighScores() 
    {
        for(int i =0; i< highScoreList.highScoreEntries.Count; i++) 
        {
            highScoreObjects[i].UpdateHighScore(highScoreList.highScoreEntries[i], i + 1);
        }
    }
    public void UpdatePlayer1Score(int score) 
    {
        player1ScoreText.text = "Score: "+score.ToString();
    }
    public void UpdatePlayer1Timer(float time) 
    {
        int i = Mathf.RoundToInt(time);
        player1TimerText.text = "Time: " + i.ToString();
    }
    public void UpdatePlayer2Score(int score)
    {
        player2ScoreText.text = "Score: " + score.ToString();
    }
    public void UpdatePlayer2Timer(float time)
    {
        int i = Mathf.RoundToInt(time);
        player2TimerText.text = "Time: " + i.ToString();
    }
    public void UpdateText(Text text, Vegetable[] vegetable,int numberOfVege)
    {
        int i = numberOfVege;
        switch (i)
        {
            case 1:
                text.text = vegetable[0].typeOfVegetable.ToString();
                return;
            case 2:
                text.text = vegetable[0].typeOfVegetable.ToString() + ", " + vegetable[1].typeOfVegetable.ToString();
                return;
            case 3:
                text.text = vegetable[0].typeOfVegetable.ToString() + ", " + vegetable[1].typeOfVegetable.ToString() + ", " + vegetable[2].typeOfVegetable.ToString();
                return;
            default:
                text.text = "";
                break;
        }
    }
    public void Retry() 
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    #endregion
}
