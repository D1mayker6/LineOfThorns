using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SettingsData", menuName = "Scriptable Objects/SettingsData")]
    public class SettingsData : ScriptableObject
    {
        public bool MuteMusic;
    
        public bool MuteSound;
    
        public int MusicVolume;
    
        public int SoundVolume;
    }
}
