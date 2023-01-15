using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Rocketlauncher : Weapon
{
    public Vector3 target;
    public LayerMask layersToHit;
    public float flySpeed;

    private static ParticleSystem.Particle[] particles;

    int count;

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0))
        {
            Vector3 screenPos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, layersToHit))
            {
                target = hitInfo.point;
            }
        }
    }

    private void LateUpdate()
    {
        InitializeIfNeeded();

        int NumParticlesAlive = projectileVFX.GetParticles(particles);
        
        for (int i = 0; i < NumParticlesAlive; i++)
        {
            particles[i].position = Vector3.Lerp(particles[i].position, target, flySpeed);
        }

        projectileVFX.SetParticles(particles, NumParticlesAlive);

    }

    void InitializeIfNeeded()
    {
        if (particles == null || particles.Length < projectileVFX.main.maxParticles)
        {
            particles = new ParticleSystem.Particle[projectileVFX.main.maxParticles];
        }
    }
}
