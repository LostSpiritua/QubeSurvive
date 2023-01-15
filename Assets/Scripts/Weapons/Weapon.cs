using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    public float attackPower;
    public float fireRate;
    public ParticleSystem projectileVFX;
    
    protected float timeUntilNextShoot = 0f;
    
    public virtual void Update()
    {
        if (Input.GetMouseButton(0) && timeUntilNextShoot < Time.time) 
        {
            Shoot();
            timeUntilNextShoot = Time.time + fireRate;
        }
    }

    public virtual void Shoot()
    {
        projectileVFX.Play();
    }
}
