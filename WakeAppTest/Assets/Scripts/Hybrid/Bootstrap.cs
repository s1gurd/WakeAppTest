using Scripts.SceneManagement;
using Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Hybrid
{
    public sealed class Bootstrap
    {
        public static Settings settings;
        public static CreateLevel Level;
        public static Camera camera;

        public static async void InitGame(Settings s, CreateLevel level)
        {
            settings = s;
            
            Level = level;
            
            await Level.MakeLevel();
            
            camera = Camera.main;
            Assert.IsNotNull(camera);
            
            var setter = camera.gameObject.GetComponent<CameraSetDistance>();
            if (setter == null)
            {
                setter = camera.gameObject.AddComponent<CameraSetDistance>();
            }
            setter.SetDistance();
            
            var player = Object.Instantiate(settings.PlayerPrefab,
                new Vector3(0f, 0f, Level.levelBounds.TopLeft.z + settings.LevelSettings.PlayerSpawningOffset),
                Quaternion.LookRotation(Vector3.forward));
            Assert.IsNotNull(player);
            
            var gun = Object.Instantiate(settings.GunPrefab, player.GetComponent<PlayerObject>().GunPivot, false);
            Assert.IsNotNull(gun);
            
            setter.MoveCamera();
        }
        
    }
}