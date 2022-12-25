using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    
    private Player playerScrpt; // Access to Player variables
    private float panBorderThickness = 0.17f; // Space in pixels between screen edge and border when camera start moving by player
    private float cameraStopPos = 15.5f; // Point on map at the bound when camera should stop moving
    
    private void Start()
    {
        playerScrpt = player.GetComponent<Player>();     
        
    }
    private void LateUpdate()
    {
        CameraMoveByPlayer(cameraStopPos);
        
    }

    // Move main camera by player
    private void CameraMoveByPlayer(float mapBounds)
    {        
        Vector3 pos = player.transform.position;
        if (pos.x > mapBounds && pos.z > mapBounds)
        {
            MoveLeft();
            MoveDown();
            return;
        }
        if (pos.x < -mapBounds && pos.z > mapBounds)
        {
            MoveRight();
            MoveDown();
            return;
        }
        if (pos.x > mapBounds && pos.z < -mapBounds)
        {
            MoveLeft();
            MoveUp();
            return;
        }
        if (pos.x < -mapBounds && pos.z < -mapBounds)
        {
            MoveUp();
            MoveRight();
            return;
        }

        if (pos.x > mapBounds)
        {
            MoveDown();
            MoveUp();
            MoveLeft();
            return;
        } 
        if (pos.x < -mapBounds)
        {
            MoveDown();
            MoveUp();
            MoveRight();
            return;
        }
        if (pos.z > mapBounds)
        {
            MoveDown();
            MoveRight();
            MoveLeft();
            return;
        }
        if (pos.z < -mapBounds)
        {
            MoveRight();
            MoveLeft();
            MoveUp();
            return;
        }

        MoveRight();
        MoveLeft();
        MoveUp();
        MoveDown();
    }

    // Move camera when player at screen edge
    private void MoveRight()
    {
        Vector3 playerPosOnScreen = Camera.main.WorldToViewportPoint(player.transform.position);
        if (playerPosOnScreen.x >= (1 - panBorderThickness))
        {
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * playerScrpt.playerSpeed);
        }
    }
    // Move camera when player at screen edge
    private void MoveLeft()
    {
        Vector3 playerPosOnScreen = Camera.main.WorldToViewportPoint(player.transform.position);
        if (playerPosOnScreen.x <= panBorderThickness)
        {
            gameObject.transform.Translate(Vector3.left * Time.deltaTime * playerScrpt.playerSpeed);
        }
    }
    // Move camera when player at screen edge
    private void MoveUp()
    {
        Vector3 playerPosOnScreen = Camera.main.WorldToViewportPoint(player.transform.position);
        if (playerPosOnScreen.y >= (1 - panBorderThickness))
        {
            gameObject.transform.Translate(Vector3.up * Time.deltaTime * playerScrpt.playerSpeed);
        }
    }
    // Move camera when player at screen edge
    private void MoveDown()
    {
        Vector3 playerPosOnScreen = Camera.main.WorldToViewportPoint(player.transform.position);
        if (playerPosOnScreen.y <= panBorderThickness)
        {
            gameObject.transform.Translate(Vector3.down * Time.deltaTime * playerScrpt.playerSpeed);
        }
    }
}
