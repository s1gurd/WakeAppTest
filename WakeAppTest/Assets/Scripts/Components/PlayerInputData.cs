using Unity.Entities;
using Unity.Mathematics;

namespace Scripts.Components
{
    public struct PlayerInputData : IComponentData
    {
        public float2 Move;
    }
}