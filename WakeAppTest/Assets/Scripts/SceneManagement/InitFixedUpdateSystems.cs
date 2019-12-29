using Scripts.Systems;
using Unity.Entities;
using UnityEngine;

namespace Scripts.SceneManagement
{
    public class InitFixedUpdateSystems:MonoBehaviour
    {
        private PlayerInputSystem playerInputSystem;

        private void Start()
        {
            playerInputSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<PlayerInputSystem>();
        }

        private void FixedUpdate()
        {
            playerInputSystem.Update();

        }
        
    }
}