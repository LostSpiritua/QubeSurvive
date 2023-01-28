using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Range(0.0f, 1.0f)]
    public float volumeEffects;
    [Range(0.0f, 1.0f)]
    public float volumeMusic;

    public Sound[] sound;

    private Dictionary<string, float> soundTimerDictionary;
    private ObjectPooler pool;

   private void Awake()
   {
       if (Instance == null)
       {
           Instance = this;
           DontDestroyOnLoad(gameObject);
       }
       else
       {
           Destroy(gameObject);
       }
   }

    private void Start()
    {
        pool = GameObject.FindObjectOfType<ObjectPooler>();

        soundTimerDictionary = new Dictionary<string, float>();

        foreach (Sound s in sound)
        {
            soundTimerDictionary.Add(s.name, 0f);
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;   
            s.source.loop = s.loop;
        }
    }


    public void Play(string name, Vector3 pos, float time, float delay)
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (CanPlaySound(name, delay))
        {
            GameObject sounder = pool.SpawnFromPool("sound", pos, Quaternion.identity);
            
            s.source = sounder.GetComponent<AudioSource>();
            s.source.clip = s.clip;

            if (s.name == "music")
            {
                s.source.volume = volumeMusic;
            }
            else
            {
                s.source.volume = s.volume;
            }

            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
           // s.source.spatialBlend = 0.7f;
           // s.source.spread = 300f;

            if (time > 0)
            {
                StartCoroutine(PlayForTime(time, s.source));
            }
            else s.source.Play();            
        }

    }

    private bool CanPlaySound(string name, float delay)
    {
        if (soundTimerDictionary.ContainsKey(name))
        {
            float lastTimePlayed = soundTimerDictionary[name];
            if (lastTimePlayed + delay < Time.time)
            {
                soundTimerDictionary[name] = Time.time;
                return true;
            }
            else return false;
        }
        return true;
    }

    private System.Collections.IEnumerator PlayForTime(float time, AudioSource s)
    {
        s.Play();

        yield return new WaitForSeconds(time);

        s.Stop();
    }
}
