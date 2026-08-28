using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioChannel", menuName = "Audio/AudioChannel")]
public class AudioChannel : ScriptableObject
{
    public event Action<AudioClip> OnPlayRequested;

    public AudioClip[] Sounds;

    public void Play()
    {
        int random = UnityEngine.Random.Range(0, Sounds.Length);
        OnPlayRequested?.Invoke(Sounds[random]);
    }

    public void SetVolume(float volume)
    {
        
    }
}
