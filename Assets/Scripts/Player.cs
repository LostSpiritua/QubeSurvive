using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public float playerHealth = 100.0f;                                 // Player's health  
    public float playerArmor = 50.0f;                                   // Player's Armor
    public float playerSpeed = 2.0f;                                    // Player's move speed
    public float armorRestoreRate = 1.0f;                               // Control how fast player restore armor
    public float playerSpeedDefault;                                    // Player's default speed
    public List<GameObject> weapons = new List<GameObject>();           // Weapon list 
    public Weapon currentWeapon;                                        // Weapon that player using now
    public Color damageColor;                                           // Color at damage
    public bool armorBonus = false;                                     // Freeze armor at default value if true
    public LayerMask layersToHit;                                       // Layers for ray function from cursor to ground
    public int startWep;                                                // Starting weapon index

    private float playerArmorDefault;                                   // Variable for saving starting value of armor
    private float playerHealthDefault;                                  // Variable for saving starting value of health
    private HudUpdate HUD;                                              // Access to HUDUpdate script    
    private Renderer playerMat;                                         // Player material
    private Color playerColor;                                          // Player material's color
    private LvlControl C;
    // Start is called before the first frame update
    void Start()
    {
        C = GameObject.Find("LvlControl").GetComponent<LvlControl>();
        playerSpeedDefault = playerSpeed; 
        playerArmorDefault = playerArmor;
        playerHealthDefault = playerHealth;
        HUD = GameObject.Find("MainHUD").GetComponent<HudUpdate>();
        playerMat = gameObject.GetComponentInChildren<Renderer>();
        playerColor = playerMat.material.GetColor("_BaseColor");
        SetPlayerWeapon(startWep);
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.gameStarted)
        {
            LookTowardMouse();
            PlayerMove();
            ArmorRestore();
        }
    }


    // Rotate player relativly cursor position
    private void LookTowardMouse()
    {
        // Mouse position relativly of screen
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Direction in World relativly mouse position
        // Vector3 lookDirection = new Vector3(mousePos.x, gameObject.transform.position.y, mousePos.z);

        var mousePos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, layersToHit))
        {
            var lookit = new Vector3(hitInfo.point.x, gameObject.transform.position.y, hitInfo.point.z);

            gameObject.transform.GetChild(0).transform.LookAt(lookit);
        }



    }

    // Player controll by WASD or Arrows
    public void PlayerMove()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(inputX, 0f, inputY).normalized;

        gameObject.transform.Translate(inputDirection * playerSpeed * Time.deltaTime, Space.World);

       // if (inputX != 0f || inputY != 0f) 
       // {
       //     SoundManager.Instance.StepSound(playerSpeed, 0.4f, transform.position); 
       // }
    }


    // Restore Armor by time
    private void ArmorRestore()
    {
        if (playerArmor < playerArmorDefault)
        {
            playerArmor += Time.deltaTime * armorRestoreRate;
        }
    }

    // Health damage
    public void HealthDamage(float attackPower)
    {
        if (!armorBonus)
        {
            float attack = -playerArmor + attackPower;

            if (attack > 0)
            {
                playerHealth -= attack;
                StartCoroutine(TakeDamege(damageColor));
                SoundManager.Instance.Play("punch", transform.position, 0f, 0.1f);
            }
            else
            {
                SoundManager.Instance.Play("block", transform.position, 0f, 0.5f);
                StartCoroutine(TakeDamege(Color.white));
            }
            playerArmor -= attackPower;

            if (playerArmor < 0)
            {
                playerArmor = 0;
            }

            if (playerHealth <= 0)
            {
                C.LivesAction(-1);
                HUD.LivesHUDUpdate();
                if (!(GameManager.Instance.Lives < 0))
                {
                    playerHealth = playerHealthDefault;
                }
            }

        }

    }

    // Heal player for healPoint value
    public void Heal(float healPoint)
    {
        playerHealth += healPoint;

        if (playerHealth < playerHealthDefault)
        {
            playerHealth = playerHealthDefault;
        }
        HUD.BarUpdate();
    }

    // Change player's material color to another color with some delay
    IEnumerator TakeDamege(Color color)
    {
        playerMat.material.SetColor("_BaseColor", color);

        yield return new WaitForSeconds(0.1f);

        playerMat.material.SetColor("_BaseColor", playerColor);
    }

    // Set player weapon by weapon index from weapon list
    public void SetPlayerWeapon(int weaponID)
    {
        currentWeapon = weapons[weaponID].GetComponent<Weapon>();

        foreach (GameObject Weapon in weapons) 
        {
            Weapon.SetActive(false);
        }
        weapons[weaponID].SetActive(true);

        HUD.UpdateActiveWeapon(weaponID);
    }
}
