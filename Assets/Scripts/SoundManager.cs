using System;
using System.Collections.Generic;
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
        Instance = this;
                   
        pool = GameObject.FindObjectOfType<ObjectPooler>();

        soundTimerDictionary = new Dictionary<string, float>();

        foreach (Sound s in sound)
        {
            if (!soundTimerDictionary.ContainsKey(s.tagename))
            {
                soundTimerDictionary.Add(s.tagename, 0f);
            }
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
    }


    public void Play(string name, Vector3 pos, float time, float delay)
    {
        Sound s = Array.Find(sound, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (CanPlaySound(s.tagename, delay))
        {
            GameObject sounder = pool.SpawnFromPool("Sound", pos, Quaternion.identity);

            s.source = sounder.GetComponent<AudioSource>();
            s.source.clip = s.clip;

            if (s.name == "music")
            {
                s.source.volume = volumeMusic;
            }
            else
            {
                s.source.volume = PercentFromGlobalVolume(s.volume);
            }

            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            // s.source.spatialBlend = 0.7f;
            // s.source.spread = 300f;

            if (time > 0)
            {
                StartCoroutine(PlayForTime(time, s.source));
            }
            else StartCoroutine(PlayForTime(s.source.clip.length, s.source));



        }

    }

    private bool CanPlaySound(string tage, float delay)
    {
        if (soundTimerDictionary.ContainsKey(tage))
        {
            float lastTimePlayed = soundTimerDictionary[tage];
            if (lastTimePlayed + delay < Time.time)
            {
                soundTimerDictionary[tage] = Time.time;
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

    private float PercentFromGlobalVolume(float vol)
    {
        float tVol = 1 - volumeEffects;

        return vol - vol * tVol;
    }
    public void StepSound(float speed, float delay)
    {
        string[] step = { "step1", "step2", "step3", "step4", "step5", "step6" };
        int i;
        i = UnityEngine.Random.Range(0, step.Length);
        float tdelay = delay / speed;
        SoundManager.Instance.Play(step[i], gameObject.transform.position, 0, tdelay);
    }
}
