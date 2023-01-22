using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Speedup : Drop
{
    public float enchanceSpeed = 4.0f;
    private Player player;
        
    public override void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (player.playerSpeedDefault == player.playerSpeed) 
        {
            base.OnTriggerEnter(other);
        }
    }

    public override void DropBonusWork()
    {
        player.playerSpeed = enchanceSpeed;
    }

    
    public override void DropBonusAfterWork()
    {
        player.playerSpeed = player.playerSpeedDefault;
    }
    public override void NewParentForVFX(Transform other)
    {
        var otherChild = other.gameObject.transform.GetChild(0);
        effectVFX.transform.parent = otherChild.transform;
        effectVFX.transform.position = otherChild.transform.position;
        effectVFX.transform.rotation = otherChild.transform.rotation;
        effectVFX.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
    }

}
