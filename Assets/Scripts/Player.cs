using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerHealth = 100.0f; // Player's health  
    public float playerArmor = 50.0f; // Player's Armor
    public float playerSpeed = 2.0f; // Player's move speed
    public float armorRestoreRate = 1.0f; // Control how fast player restore armor
    public Weapon currentWeapon; // Weapon that player using now
    public Color damageColor; // Color at damage
    public bool armorBonus = false; // Freeze armor at default value if true

    private float playerArmorDefault; // Variable for saving starting value of armor
    private float playerHealthDefault; // Variable for saving starting value of health
    private HudUpdate HUD; // Access to HUDUpdate script    
    private Renderer playerMat; // Player material
    private Color playerColor; // Player material's color

    // Start is called before the first frame update
    void Start()
    {
        playerArmorDefault = playerArmor;
        playerHealthDefault = playerHealth;
        HUD = GameObject.Find("MainHUD").GetComponent<HudUpdate>();
        playerMat = gameObject.GetComponentInChildren<Renderer>();
        playerColor = playerMat.material.GetColor("_BaseColor");
    }

    // Update is called once per frame
    private void Update()
    {
        LookTowardMouse();
        PlayerMove();
        ArmorRestore();
    }


    // Rotate player relativly cursor position
    private void LookTowardMouse()
    {
        // Mouse position relativly of screen
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Direction in World relativly mouse position
        Vector3 lookDirection = new Vector3(mousePos.x, gameObject.transform.position.y, mousePos.z);

        transform.LookAt(lookDirection);
    }

    // Player controll by WASD or Arrows
    public void PlayerMove()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(inputX, 0f, inputY).normalized;

        gameObject.transform.Translate(inputDirection * playerSpeed * Time.deltaTime, Space.World);
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
    public void HealthDamage (float attackPower)
    {
        if (!armorBonus)
        {
            float attack = -playerArmor + attackPower;

            if (attack > 0)
            {
                playerHealth -= attack;
                StartCoroutine(TakeDamege(damageColor));
            }
            else StartCoroutine(TakeDamege(Color.white));

            playerArmor -= attackPower;

            if (playerArmor < 0)
            {
                playerArmor = 0;
            }

            if (playerHealth <= 0)
            {
                GameManager.Instance.LivesAction(-1);
                HUD.LivesHUDUpdate();
                if (!(GameManager.Instance.Lives < 0)) 
                {
                    playerHealth = playerHealthDefault;
                }
            }

        }

    }

    // Heal player for healPoint value
    public void Heal (float healPoint)
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
}
