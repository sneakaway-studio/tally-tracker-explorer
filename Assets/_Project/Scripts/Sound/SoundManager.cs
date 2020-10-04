using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// reference https://www.daggerhart.com/unity-audio-and-sound-manager-singleton-script/


public class SoundManager : Singleton<SoundManager> {
    // singleton
    protected SoundManager () { }
    //public static new PlayerManager Instance;


    public AudioClip [] effects;
    public AudioClip [] music;

    // Audio players components.
    public AudioSource EffectsSource;
    public AudioSource MusicSource;

    // Random pitch adjustment range.
    public float LowPitchRange = .95f;
    public float HighPitchRange = 1.05f;


    public void PlayEffectIndex (int index)
    {
        if (effects.Length <= index - 1)
            Play (effects [index]);
    }
    public void PlayMusicIndex (int index)
    {
        if (effects.Length <= index - 1)
            PlayMusic (music [index]);
    }
    public void RandomSoundEffectIndex ()
    {
        RandomSoundEffect (effects);
    }





    // Play a single clip through the sound effects source.
    public void Play (AudioClip clip)
    {
        EffectsSource.clip = clip;
        EffectsSource.PlayOneShot (clip);
    }

    // Play a single clip through the music source.
    public void PlayMusic (AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play ();
    }

    // Play a random clip from an array, and randomize the pitch slightly.
    public void RandomSoundEffect (params AudioClip [] clips)
    {
        int randomIndex = Random.Range (0, clips.Length);
        float randomPitch = Random.Range (LowPitchRange, HighPitchRange);

        EffectsSource.pitch = randomPitch;
        EffectsSource.clip = clips [randomIndex];
        EffectsSource.PlayOneShot (EffectsSource.clip);
    }

}