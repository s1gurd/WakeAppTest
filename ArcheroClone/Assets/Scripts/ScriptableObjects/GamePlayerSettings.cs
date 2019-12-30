using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Settings", menuName = "Player Settings", order = 81)]
    public class GamePlayerSettings:ScriptableObject, IActorSettings
    {
        [SerializeField] private float _playerMoveSpeed = 6f;
        [SerializeField] private float _turningSpeed = 50f;
        [SerializeField] private int _playerHealth = 100;
        [SerializeField] private MovementType _movementType = MovementType.Walking;
        
        public float MoveSpeed
        {
            get => _playerMoveSpeed;
            set => _playerMoveSpeed = value;
        }

        public float TurningSpeed
        {
            get => _turningSpeed;
            set => _turningSpeed = value;
        }

        public int Health
        {
            get => _playerHealth;
            set => _playerHealth = value;
        }

        public MovementType MovementType
        {
            get => _movementType;
            set => _movementType = value;
        }
    }
}