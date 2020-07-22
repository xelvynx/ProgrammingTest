using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighScoreUI : MonoBehaviour
{
    #region Variables
public Text positionText;
    public Text scoreText;
    public Text playerNameText;

    #endregion
    
    public void UpdateHighScore(HighscoreEntry hsEntry, int position)
    {
        Debug.Log("updating high score");
        positionText.text = position.ToString();
        scoreText.text = hsEntry.score.ToString();
        playerNameText.text = hsEntry.playerName;
    }
}
