using UnityEngine.Audio;
using System;
using UnityEngine;

// Source: Brackeys
// https://www.youtube.com/watch?v=6OT43pvUyfY&ab_channel=Brackeys

public class AudioManager : MonoBehaviour {
    // for Singleton
    public static AudioManager Instance;
    // main mixer
    public AudioMixerGroup mixerGroup;

    public Sound [] sounds;

    void Awake ()
    {
        if (Instance != null) {
            Destroy (gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad (gameObject);
        }

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource> ();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            // default to main mixergroup if not set
            s.source.outputAudioMixerGroup = s.mixerGroup ? s.mixerGroup : mixerGroup;
        }
    }

    public void Play (string sound)
    {
        Debug.Log (DebugManager.GetSymbol ("sound") + " AudioManager.Play() sound =" + sound);
        // find sound in array 
        Sound s = Array.Find (sounds, item => item.name == sound);
        if (s == null) {
            Debug.LogWarning ("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range (-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range (-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.PlayOneShot (s.source.clip);
    }

}
