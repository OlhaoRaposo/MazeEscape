using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;

            s.Source.volume = s.volume;
            s.Source.pitch = s.pitch;

            s.Source.loop = s.loop;

        }
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds,sounds => sounds.name == name);
        s.Source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds,sounds => sounds.name == name);
        s.Source.Stop();
    }
    
    
}
