using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSlider_Armor : BarSlider
{
    private void Update()
    {
        HUDUpdate();
    }
    public override void HUDInitialize()
    {
        SetMaxValue(player.playerArmor);
    }

    public override void HUDUpdate()
    {
        SetValue(player.playerArmor);
    }
}
