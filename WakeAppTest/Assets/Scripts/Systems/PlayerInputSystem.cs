using Scripts.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Systems
{
    [DisableAutoCreation]
    public class PlayerInputSystem : JobComponentSystem
    {
        private EntityCommandBufferSystem barrier;

        private InputAction moveAction;

        private float2 moveInput;
        private bool walking;

        protected override void OnCreate()
        {
            barrier = World.GetOrCreateSystem<EntityCommandBufferSystem>();
        }

        protected override void OnStartRunning()
        {
            moveAction = new InputAction("move", binding: "<Gamepad>/rightStick");
            moveAction.AddCompositeBinding("Dpad")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");

            moveAction.performed += context =>
            {
                moveInput = context.ReadValue<Vector2>();
                walking = true;
            };
            moveAction.canceled += context =>
            {
                moveInput = context.ReadValue<Vector2>();
                walking = false;
            };
            moveAction.Enable();
        }

        protected override void OnStopRunning()
        {
            moveAction.Disable();
        }

        [BurstCompile]
        private struct PlayerInputJob : IJobForEachWithEntity<PlayerInputData>
        {
            public EntityCommandBuffer.Concurrent Ecb;
            
            [ReadOnly] public ComponentDataFromEntity<WalkingData> Walk;
            [ReadOnly] public ComponentDataFromEntity<AimingData> Shoot;
            
            [ReadOnly] public float2 MoveInput;
            [ReadOnly] public bool Walking;

            public void Execute(Entity entity, int index, ref PlayerInputData inputData)
            {
                inputData.Move = MoveInput;
                if (Walking)
                {
                    Ecb.RemoveComponent<AimingData>(index, entity);
                    Ecb.AddComponent<WalkingData>(index, entity, new WalkingData());
                }
                else
                {
                    Ecb.RemoveComponent<WalkingData>(index, entity);
                    Ecb.AddComponent<AimingData>(index, entity, new AimingData());
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new PlayerInputJob
            {
                Ecb = barrier.CreateCommandBuffer().ToConcurrent(),
                Walk = GetComponentDataFromEntity<WalkingData>(),
                Shoot = GetComponentDataFromEntity<AimingData>(),
                MoveInput = moveInput,
                Walking =walking
            };
            inputDeps = job.Schedule(this, inputDeps);
            barrier.AddJobHandleForProducer(inputDeps);
            return inputDeps;
        }
    }
}
