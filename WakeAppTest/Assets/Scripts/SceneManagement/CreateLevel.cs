using System;
using Scripts.Hybrid;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.SceneManagement
{
    public class CreateLevel : MonoBehaviour
    {
        private int levelSizeX => Bootstrap.settings.LevelSettings.LevelSizeX;
        public int levelSizeY => Bootstrap.settings.LevelSettings.LevelSizeY;
        public float tileSize => Bootstrap.settings.LevelSettings.TileSize;

        public GameObject[] floorTiles => Bootstrap.settings.LevelSettings.FloorTiles;
        public GameObject[] wallTiles => Bootstrap.settings.LevelSettings.WallTiles;
        public GameObject[] cornerTiles => Bootstrap.settings.LevelSettings.CornerTiles;

        public GameObject[] blockingProps => Bootstrap.settings.LevelSettings.BlockingProps;
        public GameObject[] obstacleProps => Bootstrap.settings.LevelSettings.ObstacleProps;

        private Transform sceneRoot;

        public void MakeLevel()
        {
            sceneRoot = gameObject.transform;
            if (floorTiles.Length == 0)
            {
                throw new Exception("Floor Tiles must contain at least 1 element!");
            }

            var pivot = new Vector3(tileSize * levelSizeX, 0f, tileSize * levelSizeY) / -2f +
                        new Vector3(tileSize / 2, 0, tileSize / 2);

            
            //Неизящный цикл расставления геометрии. Но в данном случае, это ради читаемости
            for (var i = 0; i < levelSizeY; i++)
            {
                for (var j = 0; j < levelSizeX; j++)
                {
                    var pos = new Vector3(tileSize * j, 0f, tileSize * i) + pivot;
                    var floorRot = Quaternion.Euler(new Vector3(0f, Random.Range(0, 4) * 90, 0f));
                    Instantiate(floorTiles[Random.Range(0, floorTiles.Length)], pos, floorRot, sceneRoot);
                    
                    if (i == 0 && j == 0)
                    {
                        var cornerRot = Quaternion.Euler(new Vector3(0f, 270f, 0f));
                        Instantiate(cornerTiles[Random.Range(0, wallTiles.Length)], pos, cornerRot, sceneRoot);
                        continue;
                    }
                    if (i == 0 && j == levelSizeX - 1)
                    {
                        var cornerRot = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                        Instantiate(cornerTiles[Random.Range(0, wallTiles.Length)], pos, cornerRot, sceneRoot);
                        continue;
                    }
                    if (i == levelSizeY - 1 && j == levelSizeX - 1)
                    {
                        var cornerRot = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                        Instantiate(cornerTiles[Random.Range(0, wallTiles.Length)], pos, cornerRot, sceneRoot);
                        continue;
                    }
                    if (i == levelSizeY - 1 && j == 0)
                    {
                        var cornerRot = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                        Instantiate(cornerTiles[Random.Range(0, wallTiles.Length)], pos, cornerRot, sceneRoot);
                        continue;
                    }
                    if (j == 0 || j == levelSizeX - 1)
                    {
                        var wallRotY = j == 0 ? 0 : 180;
                        var wallRot = Quaternion.Euler(new Vector3(0f, wallRotY, 0f));
                        Instantiate(wallTiles[Random.Range(0, wallTiles.Length)], pos, wallRot, sceneRoot);
                    }
                    if (i == 0 || i == levelSizeY - 1)
                    {
                        var wallRotY = i == 0 ? 270 : 90;
                        var wallRot = Quaternion.Euler(new Vector3(0f, wallRotY, 0f));
                        Instantiate(wallTiles[Random.Range(0, wallTiles.Length)], pos, wallRot, sceneRoot);
                    }
                }
            }
        }
    }
}