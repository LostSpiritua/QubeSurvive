using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public LayerMask layersToHit;

    private Player playerScrpt;                             // Access to Player variables
    private readonly float panBorderThickness = 0.45f;      // Space in pixels between screen edge and border when camera start moving by player
    private readonly float cameraStopPos = 17.3f;           // Point on map at the bound when camera should stop moving

    private void Start()
    {
        playerScrpt = player.GetComponent<Player>();

    }
    private void LateUpdate()
    {
        if (player != null)
        {
            CameraMoveByPlayer(cameraStopPos);
        }
        else
        {
            Debug.LogError("Assign Player Object to CameraMove script");
        }

    }

    // Move main camera by player
    private void CameraMoveByPlayer(float mapBounds)
    {
        Vector3 LUpoint = ScreenPos(0, Camera.main.pixelHeight - 1);
        Vector3 LBpoint = ScreenPos(0, 0);
        Vector3 RUpoint = ScreenPos(Camera.main.pixelWidth - 1, Camera.main.pixelHeight - 1);
        Vector3 RBpoint = ScreenPos(Camera.main.pixelWidth - 1, 0);

        if (LUpoint.z > mapBounds && LUpoint.x < -mapBounds)
        {
            MoveDown();
            MoveRight();
            return;
        }

        if (RUpoint.z > mapBounds && RUpoint.x > mapBounds)
        {
            MoveDown();
            MoveLeft();
            return;
        }

        if (LBpoint.z < -mapBounds && LBpoint.x < -mapBounds)
        {
            MoveUp();
            MoveRight();
            return;
        }

        if (RBpoint.z < -mapBounds && RBpoint.x > mapBounds)
        {
            MoveUp();
            MoveLeft();
            return;
        }

        if (LUpoint.x < -mapBounds || LBpoint.x < -mapBounds)
        {
            MoveDown();
            MoveRight();
            MoveUp();
            return;
        }

        if (RUpoint.x > mapBounds || RBpoint.x > mapBounds)
        {
            MoveLeft();
            MoveUp();
            MoveDown();
            return;
        }

        if (RUpoint.z > mapBounds || LUpoint.z > mapBounds)
        {
            MoveLeft();
            MoveRight();
            MoveDown();
            return;
        }

        if (RBpoint.z < -mapBounds || LBpoint.z < -mapBounds)
        {
            MoveLeft();
            MoveRight();
            MoveUp();
            return;
        }

        if (LUpoint.x > -mapBounds && RUpoint.x < mapBounds && RUpoint.z < mapBounds && RBpoint.z > -mapBounds)
        {
            MoveLeft();
            MoveUp();
            MoveRight();
            MoveDown();
            return;
        }






        //     if (pos.x > mapBounds && pos.z > mapBounds)
        //     {
        //         MoveLeft();
        //         MoveDown();
        //         return;
        //     }
        //     if (pos.x < -mapBounds && pos.z > mapBounds)
        //     {
        //         MoveRight();
        //         MoveDown();
        //         return;
        //     }
        //     if (pos.x > mapBounds && pos.z < -mapBounds)
        //     {
        //         MoveLeft();
        //         MoveUp();
        //         return;
        //     }
        //     if (pos.x < -mapBounds && pos.z < -mapBounds)
        //     {
        //         MoveUp();
        //         MoveRight();
        //         return;
        //     }
        // 
        //     if (pos.x > mapBounds)
        //     {
        //         MoveDown();
        //         MoveUp();
        //         MoveLeft();
        //         return;
        //     }
        //     if (pos.x < -mapBounds)
        //     {
        //         MoveDown();
        //         MoveUp();
        //         MoveRight();
        //         return;
        //     }
        //     if (pos.z > mapBounds)
        //     {
        //         MoveDown();
        //         MoveRight();
        //         MoveLeft();
        //         return;
        //     }
        //     if (pos.z < -mapBounds)
        //     {
        //         MoveRight();
        //         MoveLeft();
        //         MoveUp();
        //         return;
        //     }
        // 
        //     MoveRight();
        //     MoveLeft();
        //     MoveUp();
        //     MoveDown();
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

    public Vector3 ScreenPos(float X, float Y)
    {

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(X, Y));

        if (Physics.Raycast(ray, out RaycastHit hitInfo, layersToHit))
        {
            return hitInfo.point;
        }

        return Vector3.zero;
    }
}
