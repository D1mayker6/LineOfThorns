using System;
using System.Collections;
using Data;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private SettingsData _settings;
        [SerializeField] private AudioClip[] Sounds;
        
        private AudioClip _prevSound;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            _settings.OnMusicVolumeChanged += SetVolume;
            _source.volume = _settings.MusicVolume;
            PlaySound();
        }

        private void PlaySound()
        {
            StartCoroutine(PlayMusicAndCallback());
        }

        IEnumerator PlayMusicAndCallback()
        {
            AudioClip random;
            do
            {
              random = Sounds[UnityEngine.Random.Range(0, Sounds.Length)];  
            } while (random == _prevSound);
            
            _source.clip = random;
            _prevSound = random;
            _source.Play();
            while (_source.isPlaying)
                yield return null;
            
            PlaySound();
        }

        private void SetVolume(float volume)
        {
            _source.volume = volume;
        }

        private void OnDisable()
        {
            _settings.OnMusicVolumeChanged -= SetVolume;
        }
    }
}
