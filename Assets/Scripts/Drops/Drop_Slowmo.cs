using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Slowmo : Drop
{
    [SerializeField]
    private float slowmoPower = 0.5f; // Coefficient of time slow 
    [SerializeField]
    private string soundTick;
    [SerializeField]
    private string slowOutSound;
    // Slow gameplay
    public override void DropBonusWork()
    {
        SoundManager.Instance.Play(soundTick, transform.position, workTimer, 0);
        Time.timeScale = slowmoPower;
    }

    // Return default speed to game
    public override void DropBonusAfterWork()
    {  
        Time.timeScale = 1.0f;
        SoundManager.Instance.Play(slowOutSound, transform.position, 0, 0);
    }

}
