using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SettingsData", menuName = "Scriptable Objects/SettingsData")]
    public class SettingsData : ScriptableObject
    {
        public bool MuteMusic;
    
        public bool MuteSFX;
        
        private float _musicVolume;

        public float MusicVolume
        {
            get { return _musicVolume; }
            set
            {
                _musicVolume = value;
                OnMusicVolumeChanged?.Invoke(_musicVolume);
            }
        }
    
        public float SFXVolume;
        
        public event Action<float> OnMusicVolumeChanged;
    }
    
}
