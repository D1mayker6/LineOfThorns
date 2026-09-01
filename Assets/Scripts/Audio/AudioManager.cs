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
            _source.volume = 0f;
            _prevSound = random;
            _source.Play();
            while (_source.volume < _settings.MusicVolume)
            {
                _source.volume += (_settings.MusicVolume / 5f) *  Time.deltaTime;
                yield return null;
            }
            _source.volume = _settings.MusicVolume;
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

        public IEnumerator LowerPitch()
        {
            while (_source.pitch > 0.3f)
            {
                _source.pitch -= 0.5f * Time.deltaTime; 
                Debug.Log(_source.pitch);
                yield return null;
            }
            _source.pitch = 0.3f; 
        }

        public IEnumerator UpperPitch()
        {
            while (_source.pitch < 1f)
            {
                _source.pitch += 0.5f * Time.deltaTime; 
                yield return null;
            }
            _source.pitch = 1f; 
        }
    }
}
