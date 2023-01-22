using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Heal : Drop
{
    public float healPower = 50f; // Amount of restore health point
      
    private Player player;
    
    public override void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    // Heal player
    public override void DropBonusWork()
    {
        player.Heal(healPower);
    }    

    // Nothing to do after
    public override void DropBonusAfterWork()
    {
        return;
    }
}
