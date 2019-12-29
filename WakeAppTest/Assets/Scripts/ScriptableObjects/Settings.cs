using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Game Settings", order = 80)]
    public class Settings : ScriptableObject
    {
        public GameObject PlayerPrefab;
        public GameObject GunPrefab;
        
        public GamePlayerSettings PlayerSettings;
        public LevelSettings LevelSettings;
    }
}