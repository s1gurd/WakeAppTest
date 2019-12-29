using Scripts.Components;
using Scripts.Hybrid;
using Unity.Entities;
using UnityEngine;

namespace Scripts.Systems
{
    
    public class PlayerMovementSystem:ComponentSystem
    {
        private EntityQuery query;

        protected override void OnCreate()
        {
            query = GetEntityQuery(
                ComponentType.ReadOnly<Transform>(),
                ComponentType.ReadOnly<PlayerInputData>(),
                ComponentType.ReadOnly<WalkingData>(),
                ComponentType.Exclude<AimingData>(),
                ComponentType.ReadOnly<Rigidbody>());
        }

        protected override void OnUpdate()
        {
            var speed = Bootstrap.settings.PlayerSettings.MoveSpeed;

            Entities.With(query).ForEach(
                (Entity entity, Rigidbody rigidBody, ref PlayerInputData input) =>
                {
                    var movement = speed * Time.DeltaTime * Vector3.Normalize(new Vector3(input.Move.x, 0f, input.Move.y));
                    
                    var newPos = rigidBody.position + movement;
                    rigidBody.MovePosition(newPos);
                });
        }
    }
}