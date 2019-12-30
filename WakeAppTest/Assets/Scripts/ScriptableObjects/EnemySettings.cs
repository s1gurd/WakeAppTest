using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy Settings", menuName = "Enemy Settings", order = 81)]
    public class EnemySettings:ScriptableObject, IActorSettings
    {
        [SerializeField] private float _enemyMoveSpeed = 6f;
        [SerializeField] private float _turningSpeed = 50f;
        [SerializeField] private int _enemyHealth = 100;
        [SerializeField] private MovementType _movementType = MovementType.Walking;
        
        public float WalkingTime = 2f;
        public float ShootingTime = 2f;
        
        public float MoveSpeed
        {
            get => _enemyMoveSpeed;
            set => _enemyMoveSpeed = value;
        }

        public float TurningSpeed
        {
            get => _turningSpeed;
            set => _turningSpeed = value;
        }

        public int Health
        {
            get => _enemyHealth;
            set => _enemyHealth = value;
        }

        public MovementType MovementType
        {
            get => _movementType;
            set => _movementType = value;
        }
    }
}