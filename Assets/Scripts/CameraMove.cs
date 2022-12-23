using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    
    private Player playerScrpt; // Access to Player variables
    private float panBorderThickness = 50f; // Space in pixels between screen edge and border when camera start moving by player

    private void Start()
    {
        playerScrpt = player.GetComponent<Player>();     
        
    }
    private void LateUpdate()
    {
        CameraMoveByPlayer();
    }

    //Move main camera when player reach screen edge until player reach map bounds 
    private void CameraMoveByPlayer()
    {
        Vector3 playerPosOnScreen = Camera.main.WorldToScreenPoint(player.transform.position);
        Vector3 pos = gameObject.transform.position;


        if (playerPosOnScreen.x >= (Screen.width - panBorderThickness))
        {
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * playerScrpt.playerSpeed);
        }
        if (playerPosOnScreen.x <= panBorderThickness)
        {
            gameObject.transform.Translate(Vector3.left * Time.deltaTime * playerScrpt.playerSpeed);
        }
        if (playerPosOnScreen.y >= (Screen.height - panBorderThickness))
        {
            gameObject.transform.Translate(Vector3.up * Time.deltaTime * playerScrpt.playerSpeed);
        }
        if (playerPosOnScreen.y <= panBorderThickness)
        {
            gameObject.transform.Translate(Vector3.down * Time.deltaTime * playerScrpt.playerSpeed);
        }
    }
}
