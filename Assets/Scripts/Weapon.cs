using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float attackPower;
    public ParticleSystem weaponVFX;


    // Start is called before the first frame update
    void Start()
    {
        
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

    public void ShootStart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (weaponVFX != null)
            {
                weaponVFX.Emit(1);
                weaponVFX.Play();
            }       
        }       
    }

    public void ShootStop()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(weaponVFX != null)
            {
                weaponVFX.Stop();
            }
        }
    }

 }
