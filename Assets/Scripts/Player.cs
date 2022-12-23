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

    private float mapBounds = 16.0f; // Bounds of square map
    private float playerArmorDefault; // Variable for save starting value of armor
    // Start is called before the first frame update
    void Start()
    {
        playerArmorDefault = playerArmor;
    }

    // Update is called once per frame
    void Update()
    {
        LookTowardMouse();
        StopPlayerAtBound();
        PlayerMove(); 
        ArmorRestore();
        
    }

    private void FixedUpdate()
    {
        
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

    // Not allow player move out of map
    private void StopPlayerAtBound()
    {
        Vector3 pos = gameObject.transform.position;

        if (pos.x > mapBounds)
        {
            transform.position = new Vector3(mapBounds, pos.y, pos.z);
        }
        if (pos.x < -mapBounds)
        {
            transform.position = new Vector3(-mapBounds, pos.y, pos.z);
        }
        if (pos.z > mapBounds)
        {
            transform.position = new Vector3(pos.x, pos.y, mapBounds);
        }
        if (pos.z < -mapBounds)
        {
            transform.position = new Vector3(pos.x, pos.y, -mapBounds);
        }
        if (pos.x > mapBounds && pos.z > mapBounds)
        {
            transform.position = new Vector3(mapBounds, pos.y, mapBounds);
        }
        if (pos.x < -mapBounds && pos.z > mapBounds)
        {
            transform.position = new Vector3(-mapBounds, pos.y, mapBounds);
        }
        if (pos.x > mapBounds && pos.z < -mapBounds)
        {
            transform.position = new Vector3(mapBounds, pos.y, -mapBounds);
        }
        if (pos.x < -mapBounds && pos.z < -mapBounds)
        {
            transform.position = new Vector3(-mapBounds, pos.y, -mapBounds);
        }
    }

    private void ArmorRestore()
    {
        if (playerArmor < playerArmorDefault)
        {
        playerArmor += Time.deltaTime * armorRestoreRate;
        }
    }

}
