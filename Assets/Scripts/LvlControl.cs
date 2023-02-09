using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlControl : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject gameoverMenu;
    public GameObject soundctrlMenu;


    private GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    // GameOver method by scaling time to zero
    public void GameOver()
    {
        GM.gameOver = true;
        GM.gameStarted = false;
        Time.timeScale = 0f;
        GM.Lives = 2;
        gameoverMenu.SetActive(true);
    }

    public void StartGame()
    {
        GM.gameStarted = true;
        Time.timeScale = 1f;
        mainMenu.SetActive(false);
        soundctrlMenu.SetActive(false);
    }

    public void Pause()
    {
        if (GM.gamePaused)
        {
            Time.timeScale = 1f;
            GM.gamePaused = false;
            GM.gameStarted = true;
            pauseMenu.SetActive(false);
            soundctrlMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            GM.gamePaused = true;
            GM.gameStarted = false;
            pauseMenu.SetActive(true);
            soundctrlMenu.SetActive(true);
        }
    }

    public void Restart()
    {
        GM.gameTime = 0f;
        GM.Lives = 3;
        GM.totalKills = 0;
        GM.scores = 0;
        GM.gameOver = false;
        GM.gamePaused = false;
        GM.gameStarted = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {

#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
        Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
    Application.Quit();
#elif (UNITY_WEBGL)
    Application.OpenURL("https://play.unity.com/u/Lostspiritual");
#endif

    }

    // Add or remove lives depend from value & check for gamer over
    public void LivesAction(int value)
    {
        GM.Lives += value;
        if (GM.Lives < 0)
        {
            GameOver();
        }

    }
}
