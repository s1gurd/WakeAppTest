using Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace Scripts.Hybrid
{
    public class PlayerGunObject : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new PlayerGunData());
        }
    }
}