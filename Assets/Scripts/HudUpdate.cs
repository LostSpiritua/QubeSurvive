using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;


public class HudUpdate : MonoBehaviour
{
    public TextMeshProUGUI scores;
    public TextMeshProUGUI totalKills;
    public TextMeshProUGUI timer;
    public BarSlider healthBar;
    public BarSlider armorBar;
    public List<GameObject> livesImage;
    
    private void Start()
    {
        ScoresUpdate(0);
        KillsUpdate(0);
    }

    private void Update()
    {
        GameTime(GameManager.Instance.gameTime);
    }

    public void ScoresUpdate(float points)
    {
        scores.text = "scores: " + points.ToString("00000000");
    }

    public void KillsUpdate(int kills)
    {
        totalKills.text = "kills: " + kills.ToString("00000000");
    }

    public void BarUpdate()
    {
        healthBar.HUDUpdate();
    }

    public void LivesMinus()
    {
        for (int i = livesImage.Count; i > 0; i--)
        {
            if (livesImage[i - 1].activeSelf)
            {
                livesImage[i-1].SetActive(false);
                return;
            }
        }
    }

    public void GameTime(float time)
    {
        int sec = System.TimeSpan.FromSeconds(time).Seconds;
        int min = System.TimeSpan.FromSeconds(time).Minutes;
        int hour = System.TimeSpan.FromSeconds(time).Hours;

        timer.text = hour.ToString("00") + " : " + min.ToString("00") + " : " + sec.ToString("00");
    }
}
