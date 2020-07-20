using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    public Text player1ScoreText;
    public Text player1TimerText;

    public void UpdatePlayer1Score(int score) 
    {
        player1ScoreText.text = "Score: "+score.ToString();
    }
    public void UpdatePlayer1Timer(float time) 
    {
        int i = Mathf.RoundToInt(time);
        player1TimerText.text = "Time: " + i.ToString();
    }

}
