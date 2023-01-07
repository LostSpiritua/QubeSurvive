using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class HudUpdate : MonoBehaviour
{
    public TextMeshProUGUI scores; // Text at HUD canvas with scores
    public TextMeshProUGUI totalKills; // Text at HUD canvas with total kills value
    public TextMeshProUGUI timer; // Text at HUD canvas with time from game start
    public BarSlider healthBar; // Health bar slider 
    public BarSlider armorBar; // Armor bar slider
    public List<GameObject> livesImage; // List of images fills of lives indicators
    
    private void Start()
    {
        ScoresUpdate(0);
        KillsUpdate(0);
    }

    private void Update()
    {
        GameTime(GameManager.Instance.gameTime);
    }

    // Update score value on screen
    public void ScoresUpdate(float points)
    {
        scores.text = "scores: " + points.ToString("00000000");
    }

    // Update kills value on screen
    public void KillsUpdate(int kills)
    {
        totalKills.text = "kills: " + kills.ToString("00000000");
    }

    // Update any bar value on screen
    public void BarUpdate()
    {
        healthBar.HUDUpdate();
    }

    // Convert TIME ti string and update it on screen
    public void GameTime(float time)
    {
        int sec = System.TimeSpan.FromSeconds(time).Seconds;
        int min = System.TimeSpan.FromSeconds(time).Minutes;
        int hour = System.TimeSpan.FromSeconds(time).Hours;

        timer.text = hour.ToString("00") + " : " + min.ToString("00") + " : " + sec.ToString("00");
    }

    // Update lives indicator fill to value from GM
    public void LivesHUDUpdate()
    {
        int lives = GameManager.Instance.Lives;

        for (int i = 0; i < livesImage.Count; i++)
        {
            if (i <= lives)
            {
                livesImage[i].SetActive(true);
            } 
            else if (i > lives)
            {
                livesImage[i].SetActive(false);
            } 

        }
    }
}
