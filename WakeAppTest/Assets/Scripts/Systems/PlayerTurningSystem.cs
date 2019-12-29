using Scripts.Components;
using Scripts.Hybrid;
using Unity.Entities;
using UnityEngine;

namespace Scripts.Systems
{
    
    public class PlayerTurningSystem : ComponentSystem
    {
        private EntityQuery query;

        protected override void OnCreate()
        {
            query = GetEntityQuery(
                ComponentType.ReadOnly<Transform>(),
                ComponentType.ReadOnly<PlayerData>(),
                ComponentType.ReadOnly<PlayerInputData>(),
                ComponentType.ReadOnly<Rigidbody>(),
                ComponentType.ReadOnly<WalkingData>(),
                ComponentType.Exclude<AimingData>());
        }

        protected override void OnUpdate()
        {
            var turningSpeed = Bootstrap.settings.PlayerSettings.TurningSpeed;

            Entities.With(query).ForEach((Entity entity, ref PlayerInputData input, Rigidbody rigidBody) =>
            {
                var dir = new Vector3(input.Move.x, 0, input.Move.y);
                if (dir == Vector3.zero) return;
                var Rot = rigidBody.rotation;
                var newRot = Quaternion.LookRotation(Vector3.Normalize(dir));
                if (newRot == Rot) return;
                rigidBody.MoveRotation(Quaternion.Lerp(Rot, newRot, Time.DeltaTime * turningSpeed));
            });
        }
    }
}