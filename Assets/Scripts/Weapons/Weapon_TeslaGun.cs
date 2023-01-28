using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_TeslaGun : Weapon
{
    public override void Shoot()
    {
        base.Shoot();
        SoundManager.Instance.Play("electric", gameObject.transform.position, 0.5f, 0.1f);
    }
}
