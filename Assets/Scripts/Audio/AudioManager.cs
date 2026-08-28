using System;
using Data;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioChannel _channel;
        [SerializeField] private AudioSource _source;
        [SerializeField] private SettingsData _settings;

        private void OnEnable()
        {
            _channel.OnPlayRequested += PlaySound;
            _source.volume = _settings.MusicVolume;
            DontDestroyOnLoad(gameObject);
        }

        private void PlaySound(AudioClip clip)
        {
            if (_source.isPlaying && _source.clip == clip) return;
            Debug.Log("PlaySound");
            _source.clip = clip;
            _source.Play();
        }
    }
}
