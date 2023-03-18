using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers.Scene_Manager
{
    public class MySceneManager : MonoBehaviour
    {
        public void OpenScene(SceneType sceneType)
        {
            switch (sceneType)
            {
                case SceneType.Menu:
                    OpenMenu();
                    break;
                case SceneType.Game:
                    OpenGame();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sceneType), sceneType, null);
            }
        }

        private void OpenMenu()
        {
            SceneManager.LoadSceneAsync((int) SceneType.Menu);
        }
        
        private void OpenGame()
        {
            SceneManager.LoadSceneAsync((int) SceneType.Game);
        }
    }
}
