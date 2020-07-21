using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Highscore List", menuName = "Highscore List")]
public class HighScoreList : ScriptableObject
{
    public List<HighscoreEntry> highScoreEntries;
    public int highScoreMax = 10;
    private HighscoreEntry temporary;

    public void AddTolist(Player player)
    {
        temporary = new HighscoreEntry(player.name, player.Score);
        highScoreEntries.Add(temporary);
        SortList();
    }
    public void SortList()
    {
        highScoreEntries.Sort((s1, s2) => s1.score.CompareTo(s2.score));
        highScoreEntries.Reverse();
        if (highScoreEntries.Count > highScoreMax)
        {
            highScoreEntries.RemoveRange(10, highScoreEntries.Count - highScoreMax);
        }
    }
}
