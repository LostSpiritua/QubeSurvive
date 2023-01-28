using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSound : MonoBehaviour
{
    public string spawnSound;
    public float timeS;
    public float delayS;
    public string dissapearSound;
    public float timeD;
    public float delayD;


    private ParticleSystem parentParticleSystem;

    private IDictionary<uint, ParticleSystem.Particle> trackedParticles = new Dictionary<uint, ParticleSystem.Particle>();

    void Start()
    {
        parentParticleSystem = this.GetComponent<ParticleSystem>();
        if (parentParticleSystem == null)
            Debug.LogError("Missing ParticleSystem!", this);
    }

    void FixedUpdate()
    {
        var liveParticles = new ParticleSystem.Particle[parentParticleSystem.particleCount];
        parentParticleSystem.GetParticles(liveParticles);

        var particleDelta = GetParticleDelta(liveParticles);

        if (spawnSound != "0")
        {
            foreach (var particleAdded in particleDelta.Added)
            {
                SoundManager.Instance.Play(spawnSound, particleAdded.position, timeS, delayS);

                //Debug.Log($"New particle spawned '{particleAdded.randomSeed}' at position '{particleAdded.position}'");
            }
        }

        if (dissapearSound != "0")
        {
            foreach (var particleRemoved in particleDelta.Removed)
            {
                SoundManager.Instance.Play(dissapearSound, particleRemoved.position, timeD, delayD);
                //Todo: Play "Disappear" sound - use particleRemoved.position to play at right position
               // Debug.Log($"Particle despawned '{particleRemoved.randomSeed}' at position '{particleRemoved.position}'");
            }
        }
    }

    private ParticleDelta GetParticleDelta(ParticleSystem.Particle[] liveParticles)
    {
        var deltaResult = new ParticleDelta();

        foreach (var activeParticle in liveParticles)
        {
            ParticleSystem.Particle foundParticle;
            if (trackedParticles.TryGetValue(activeParticle.randomSeed, out foundParticle))
            {
                trackedParticles[activeParticle.randomSeed] = activeParticle;
            }
            else
            {
                deltaResult.Added.Add(activeParticle);
                trackedParticles.Add(activeParticle.randomSeed, activeParticle);
            }
        }

        var updatedParticleAsDictionary = liveParticles.ToDictionary(x => x.randomSeed, x => x);
        var dictionaryKeysAsList = trackedParticles.Keys.ToList();

        foreach (var dictionaryKey in dictionaryKeysAsList)
        {
            if (updatedParticleAsDictionary.ContainsKey(dictionaryKey) == false)
            {
                deltaResult.Removed.Add(trackedParticles[dictionaryKey]);
                trackedParticles.Remove(dictionaryKey);
            }
        }

        return deltaResult;
    }

    private class ParticleDelta
    {
        public IList<ParticleSystem.Particle> Added { get; set; } = new List<ParticleSystem.Particle>();
        public IList<ParticleSystem.Particle> Removed { get; set; } = new List<ParticleSystem.Particle>();
    }
}
