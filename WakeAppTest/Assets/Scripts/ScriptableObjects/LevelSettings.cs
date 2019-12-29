using UnityEngine;

namespace Scripts.ScriptableObjects
{
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
    }
}