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
    public bool gameOver = false;

    private BarSlider healthBarSlider;
    private BarSlider armorBarSlider;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        CheckForExist();
        HUDInitialize();
        
    }

    // Update is called once per frame
    void Update()
    {
        HUDUpdate();
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

    void HUDUpdate()
    {
        if (healthBarSlider != null)
        {
            healthBarSlider.SetValue (player.playerHealth);
        }

        if (armorBarSlider != null)
        {
            armorBarSlider.SetValue(player.playerArmor);
        }
    }

    void HUDInitialize ()
    {
        healthBarSlider = GameObject.Find("HealthBar").GetComponent<BarSlider>();
        armorBarSlider = GameObject.Find("ArmorBar").GetComponent<BarSlider>();

        armorBarSlider.SetMaxValue(player.playerArmor);
        healthBarSlider.SetMaxValue(player.playerHealth);
    }
}
