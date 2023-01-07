using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Slowmo : Drop
{
    [SerializeField]
    private float slowmoPower = 0.5f; // Coefficient of time slow 

    // Slow gameplay
    public override void DropBonusWork()
    {
        Time.timeScale = slowmoPower;
    }

    // Return default speed to game
    public override void DropBonusAfterWork()
    {  
        Time.timeScale = 1.0f;
    }

}
