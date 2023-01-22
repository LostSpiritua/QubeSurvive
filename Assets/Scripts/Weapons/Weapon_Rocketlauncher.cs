using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Weapon_Rocketlauncher : Weapon
{
    public Vector3 target;                                                               // Rocket's target position in world
    public LayerMask layersToHit;                                                        // Layer of scene for ray trace from cursor to ground
    public float flySpeed;                                                               // Speed of rocket
                                                                                         // 
    private static ParticleSystem.Particle[] particles;                                  // Array of all instantinate particles

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0))
        {
            PositionFromCursos();
        }
    }

    private void LateUpdate()
    {
        InitializeIfNeeded();
        MoveParticlesToTarget(target);
    }

    // Check for particles alive
    void InitializeIfNeeded()
    {
        if (particles == null || particles.Length < projectileVFX.main.maxParticles)
        {
            particles = new ParticleSystem.Particle[projectileVFX.main.maxParticles];
        }
    }

    // Recieve new position for Target by cursor position
    private void PositionFromCursos()
    {
        Vector3 screenPos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, layersToHit))
        {
            target = hitInfo.point;
        }
    }

    // Move particle to target
    private void MoveParticlesToTarget (Vector3 Target)
    {
        int NumParticlesAlive = projectileVFX.GetParticles(particles);

        for (int i = 0; i < NumParticlesAlive; i++)
        {
            particles[i].position = Vector3.Lerp(particles[i].position, Target, flySpeed);
        }

        projectileVFX.SetParticles(particles, NumParticlesAlive);
    }
}
