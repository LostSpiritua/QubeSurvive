using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSlider_Armor : BarSlider
{
    private void Update()
    {
        HUDUpdate();
    }

    // Initialize armor max value
    public override void HUDInitialize()
    {
        SetMaxValue(player.playerArmor);
    }

    // Update armor value
    public override void HUDUpdate()
    {
        SetValue(player.playerArmor);
    }
}
