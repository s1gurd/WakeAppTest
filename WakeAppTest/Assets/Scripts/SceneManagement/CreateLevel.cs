using System;
using System.Threading.Tasks;
using Scripts.Hybrid;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Scripts.SceneManagement
{
    public struct LevelBounds
    {
        public Vector3 TopLeft;
        public Vector3 TopRight;
        public Vector3 BottomLeft;
        public Vector3 BottomRight;
        public LevelBounds(float delta)
        {
            TopLeft = new Vector3(0 - delta, 0f, 0 -delta);
            TopRight = new Vector3(delta, 0f,  -delta);
            BottomLeft = new Vector3(0-delta,0f,delta);
            BottomRight = new Vector3(delta,0f,delta);
        }
        
        public Vector2 TopLeft2D => new Vector2(TopLeft.x, TopLeft.z);
        public Vector2 TopRight2D => new Vector2(TopRight.x, TopRight.z);
        public Vector2 BottomLeft2D => new Vector2(BottomLeft.x, BottomLeft.z);
        public Vector2 BottomRight2D => new Vector2(BottomRight.x, BottomRight.z);
    }
    
    public class CreateLevel : MonoBehaviour
    {
        private int levelSizeX => Bootstrap.settings.LevelSettings.LevelSizeX;
        private int levelSizeY => Bootstrap.settings.LevelSettings.LevelSizeY;
        private float tileSize => Bootstrap.settings.LevelSettings.TileSize;

        private GameObject[] floorTiles => Bootstrap.settings.LevelSettings.FloorTiles;
        private GameObject[] wallTiles => Bootstrap.settings.LevelSettings.WallTiles;
        private GameObject[] cornerTiles => Bootstrap.settings.LevelSettings.CornerTiles;

        private GameObject[] blockingProps => Bootstrap.settings.LevelSettings.BlockingProps;
        private GameObject[] obstacleProps => Bootstrap.settings.LevelSettings.ObstacleProps;

        public LevelBounds levelBounds;
        
        private Transform sceneRoot;

#pragma warning disable 1998
        public async Task MakeLevel()
        {
#pragma warning restore 1998
            sceneRoot = gameObject.transform;
            levelBounds = new LevelBounds(tileSize/2);
            
            for (var i = 0; i < sceneRoot.transform.childCount; i++)
            {
                var v = sceneRoot.transform.GetChild(i);
                Destroy(v);
            }
            Assert.AreNotEqual(0, floorTiles.Length);

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
                        levelBounds.TopLeft += pos;
                        continue;
                    }
                    if (i == 0 && j == levelSizeX - 1)
                    {
                        var cornerRot = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                        Instantiate(cornerTiles[Random.Range(0, wallTiles.Length)], pos, cornerRot, sceneRoot);
                        levelBounds.TopRight += pos;
                        continue;
                    }
                    if (i == levelSizeY - 1 && j == levelSizeX - 1)
                    {
                        var cornerRot = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                        Instantiate(cornerTiles[Random.Range(0, wallTiles.Length)], pos, cornerRot, sceneRoot);
                        levelBounds.BottomRight = pos;
                        continue;
                    }
                    if (i == levelSizeY - 1 && j == 0)
                    {
                        var cornerRot = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                        Instantiate(cornerTiles[Random.Range(0, wallTiles.Length)], pos, cornerRot, sceneRoot);
                        levelBounds.BottomLeft += pos;
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