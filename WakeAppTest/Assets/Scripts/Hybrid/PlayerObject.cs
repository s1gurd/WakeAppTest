using Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Scripts.Hybrid
{
    public class PlayerObject : MonoBehaviour, IConvertGameObjectToEntity
    {
        public Transform GunPivot;
        public Entity Entity;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var settings = Bootstrap.settings;
            dstManager.AddComponentData(entity, new PlayerData());
            dstManager.AddComponentData(entity, new HealthData { Value = settings.PlayerSettings.Health });
            dstManager.AddComponentData(entity, new PlayerInputData { Move = new float2(0, 0) });

            Entity = entity;
        }
    }
}
