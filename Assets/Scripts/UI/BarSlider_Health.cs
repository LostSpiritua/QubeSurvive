using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSlider_Health : BarSlider
{
    // Initialize health max value
    public override void HUDInitialize()
    {
        SetMaxValue(player.playerHealth);
    }

    // Update health value
    public override void HUDUpdate()
    {
        SetValue(player.playerHealth);
    }
}
