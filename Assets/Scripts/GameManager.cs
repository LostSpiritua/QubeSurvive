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
    public bool gamePaused = false;           // Check for pause
    public bool gameStarted = false;          // Check for first start
    public float musicVolume = 1f;
    public float soundVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        CheckForExist();
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime; // TEMP
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
}
