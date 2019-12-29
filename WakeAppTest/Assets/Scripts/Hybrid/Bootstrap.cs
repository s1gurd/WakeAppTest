using Scripts.SceneManagement;
using Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Hybrid
{
    public sealed class Bootstrap
    {
        public static Settings settings;
        public static CreateLevel levelCreator;
        public static void InitGame(Settings s, CreateLevel level)
        {
            settings = s;
            levelCreator = level;
            levelCreator.MakeLevel();
            var player = Object.Instantiate(settings.PlayerPrefab);
            var gun = Object.Instantiate(settings.GunPrefab, player.GetComponent<PlayerObject>().GunPivot, false);
        }
        
    }
}