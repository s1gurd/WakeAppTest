using Scripts.Components;
using Scripts.Interfaces;
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
            switch (settings.PlayerSettings.MovementType)
            {
                case MovementType.Walking:
                    dstManager.AddComponentData(entity, new WalkingData());
                    break;
                case MovementType.Flying:
                    dstManager.AddComponentData(entity, new FlyingData());
                    break;
            }

            Entity = entity;
        }
    }
}
