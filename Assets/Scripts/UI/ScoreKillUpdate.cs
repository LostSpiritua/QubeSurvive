using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKillUpdate : MonoBehaviour
{
    public TextMeshProUGUI scores;                          // Text at HUD canvas with scores
    public TextMeshProUGUI totalKills;                      // Text at HUD canvas with total kills value

    private void OnEnable()
    {
        
        ScoresUpdate(GameManager.Instance.scores);
        KillsUpdate(GameManager.Instance.totalKills);
    }

    // Update score value on screen
    public void ScoresUpdate(float points)
    {
        scores.text = "scores: " + points;
    }

    // Update kills value on screen
    public void KillsUpdate(int kills)
    {
        totalKills.text = "kills: " + kills;
    }
}
