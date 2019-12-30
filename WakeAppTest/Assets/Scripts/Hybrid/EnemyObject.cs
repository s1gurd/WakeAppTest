using System;
using Scripts.Components;
using Scripts.Interfaces;
using Scripts.ScriptableObjects;
using Unity.Entities;
using UnityEngine;

namespace Scripts.Hybrid
{
    public class EnemyObject : MonoBehaviour, IConvertGameObjectToEntity
    {
        public Entity Entity;
        public EnemySettings EnemySettings;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var settings = Bootstrap.settings;
            dstManager.AddComponentData(entity, new EnemyData());
            dstManager.AddComponentData(entity, new HealthData {Value = EnemySettings.Health});
            switch (EnemySettings.MovementType)
            {
                case MovementType.Walking:
                    dstManager.AddComponentData(entity, new WalkingData());
                    break;
                case MovementType.Flying:
                    dstManager.AddComponentData(entity, new FlyingData());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Entity = entity;
        }
    }
}