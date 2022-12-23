using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float playerSpeed = 1.0f; // Player's move speed

    private float mapBounds = 16.0f; // Bounds of square map

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        LookTowardMouse();
        StopPlayerAtBound();
        PlayerMove(); 
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

}
