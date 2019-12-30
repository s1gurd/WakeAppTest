using Scripts.Hybrid;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scripts.SceneManagement
{
    public class Launcher:MonoBehaviour
    {
        public Settings settings;
        public CreateLevel levelCreator;

        private void Start()
        {
            Bootstrap.InitGame(settings, levelCreator);    
        }
        

    }
}