using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSlider_Health : BarSlider
{
    public override void HUDInitialize()
    {
        SetMaxValue(player.playerHealth);
    }

    public override void HUDUpdate()
    {
        SetValue(player.playerHealth);
    }
}
