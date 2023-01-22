using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int lives = 2;                    // Player live value
        
    public static GameManager Instance;       // Public variable for GM
    public int Lives                          // Public variable with paramerts for player's lives
    {
        get { return lives; }
        set { lives = value < 3 ? value : 2; }
    }
    public float gameSpeed;                   // Game spedd
    public float gameTime = 0f;               // Time from gamer start
    public float scores;                      // Scores
    public int totalKills;                    // Total kills counter
    public bool gameOver = false;             // Check for Game Over

        
    // Start is called before the first frame update
    void Start()
    {
        CheckForExist();      
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime; // TEMP
        if (gameOver)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.R) && gameOver) // TEMP
        {
            gameTime = 0f;
            lives = 3;
            gameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }
    }

    // Singletone for GameManager
    void CheckForExist()
    {
        if (Instance != null)  
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Add or remove lives depend from value & check for gamer over
    public void LivesAction(int value)
    {
        Lives += value;
        if (lives < 0)
        {
            gameOver = true;
        }

    } 
    
    // GameOver method by scaling time to zero
    public void GameOver()
    {
        Time.timeScale = 0f;
    }
}
