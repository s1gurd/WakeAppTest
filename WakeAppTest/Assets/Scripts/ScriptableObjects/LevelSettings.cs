using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [Serializable]
    public struct EnemyType
    {
        public GameObject Enemy;
        public int Count;
    }
    [CreateAssetMenu(fileName = "Level Settings", menuName = "Level Settings", order = 81)]
    public class LevelSettings:ScriptableObject
    {
        public int LevelSizeX = 4;
        public int LevelSizeY = 6;
        public float TileSize = 3;

        public GameObject[] FloorTiles;
        public GameObject[] WallTiles;
        public GameObject[] CornerTiles;

        public GameObject[] BlockingProps;
        public GameObject[] ObstacleProps;

        public float PlayerSpawningOffset = 3f;
        
        public EnemyType[] Enemies;
        public float EnemyZoneHeight = 12f;

        public LevelCameraSettings LevelCameraSettings;
    }
}