using Managers.Scene_Manager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu
{
    [RequireComponent(typeof(Button))]
    public class OpenGameButton : MonoBehaviour
    {
        private Button _button;


        [Inject] private MySceneManager _mySceneManager;
        
        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OpenGame);
        }
        
        private void OpenGame()
        {
            _mySceneManager.OpenScene(SceneType.Game);
        }
    }
}
