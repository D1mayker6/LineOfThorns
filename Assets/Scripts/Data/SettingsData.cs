using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SettingsData", menuName = "Scriptable Objects/SettingsData")]
    public class SettingsData : ScriptableObject
    {
        public bool MuteMusic;
    
        public bool MuteSFX;
    
        public float MusicVolume;
    
        public float SFXVolume;
    }
}
