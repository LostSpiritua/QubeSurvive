using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Speedup : Drop
{
    public float enchanceSpeed = 4.0f;
    private Player player;
    private float defaultSpeed;

    
    public override void Start()
    {
        base.Start();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Freez armor value
    public override void DropBonusWork()
    {
        defaultSpeed = player.playerSpeed;
        player.playerSpeed = enchanceSpeed;
    }

    // Unfreeze armor value
    public override void DropBonusAfterWork()
    {
        player.playerSpeed = defaultSpeed;
    }
    public override void NewParentForVFX(Transform other)
    {
        var otherChild = other.gameObject.transform.GetChild(0);
        effectVFX.transform.parent = otherChild.transform;
        effectVFX.transform.position = otherChild.transform.position;
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookDirection = new Vector3(mousePos.x, gameObject.transform.position.y, mousePos.z);
        effectVFX.transform.LookAt(lookDirection);       
    }

}
