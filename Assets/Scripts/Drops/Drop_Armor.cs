using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Armor : Drop
{
    private Player player;

    public override void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (!player.armorBonus)
        {
        base.OnTriggerEnter(other);
        }
    }

    // Freez armor value
    public override void DropBonusWork()
    {
        player.armorBonus = true;
    }

    // Unfreeze armor value
    public override void DropBonusAfterWork()
    {
        player.armorBonus = false;
    }
}
