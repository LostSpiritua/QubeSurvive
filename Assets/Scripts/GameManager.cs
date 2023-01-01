using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;

    
    public static GameManager Instance;
    public int Lives
    {
        get { return lives;}
        set { lives = value > 0 ? value : 0;}
    }
    public float gameSpeed; 
    public float gameTime;
    public float scores;
    public int totalKills;
    public bool gameOver = false;

        
    // Start is called before the first frame update
    void Start()
    {
        CheckForExist();      
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = Time.time;
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
        if (lives <= 0)
        {
            gameOver = true;
        }
    }   
}
