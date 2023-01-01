using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    public float attackPower;
    public float fireRate;
    public ParticleSystem projectileVFX;
    public ParticleSystem BeamVFX;
    public ParticleSystem collisionVFX;


    // Start is called before the first frame update
    void Start()
    {
        WeaponInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        ShootStart();
    }

    private void LateUpdate()
    {
        ShootStop();
    }

    // Start shooting vfx simulation
    public virtual void ShootStart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (projectileVFX != null)
            {
                projectileVFX.Emit(1);
                projectileVFX.Play();
            }

            if (BeamVFX != null)
            {
                BeamVFX.Emit(1);
                BeamVFX.Play();
            }

        }
    }

    // Stop shooting vfx simulation
    public virtual void ShootStop()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (projectileVFX != null)
            {
                projectileVFX.Stop();
            }
            if (BeamVFX != null)
            {
                BeamVFX.Stop();
            }
        }
    }

    // Set shooting rate value to vfx simulation
    public virtual void WeaponInitialize()
    {
        var emissionP = projectileVFX.emission;
        emissionP.rateOverTime = fireRate;

        var emissionB = BeamVFX.emission;
        emissionB.rateOverTime = fireRate;

    }

}
