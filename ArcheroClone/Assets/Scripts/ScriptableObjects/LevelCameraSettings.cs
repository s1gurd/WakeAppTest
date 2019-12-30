using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Camera Settings", menuName = "Camera Settings", order = 81)]
    public class LevelCameraSettings:ScriptableObject
    {
        public float MoveTime = 0.4f;
        public int LevelMargin = 70;
    }
}