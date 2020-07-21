using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HighscoreEntry
{
    public string playerName;
    public int score;

    public HighscoreEntry(string name, int score) 
    {
        playerName = name;
        this.score = score;
    }

}
