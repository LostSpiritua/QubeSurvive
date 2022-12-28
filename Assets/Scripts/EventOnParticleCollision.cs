using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOnParticleCollision : MonoBehaviour
{
    public string collisonVFXFromPool;
    
    private ObjectPooler pool;
    private ParticleSystem particleLauncher;
    List<ParticleCollisionEvent> collisionEvents;

    private void Start()
    {
        pool = GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>(); 
        particleLauncher= GetComponent<ParticleSystem>();
        collisionEvents= new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents (particleLauncher, other, collisionEvents);
        
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            EmitAtLocation(collisionEvents[i]);
        }
    }

    void EmitAtLocation (ParticleCollisionEvent particleCollisionEvent)
    {
        Vector3 pos = particleCollisionEvent.intersection;
        Quaternion rot;
        if (particleCollisionEvent.normal != Vector3.zero)
        {
            rot = Quaternion.LookRotation(particleCollisionEvent.normal);
        }
        else rot = Quaternion.identity;
        GameObject objVFX = pool.SpawnFromPool(collisonVFXFromPool, pos, rot);
        ParticleSystem vfx = objVFX.GetComponent<ParticleSystem>();
        vfx.Emit(1);
    }
}
