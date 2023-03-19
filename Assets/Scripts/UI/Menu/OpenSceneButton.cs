using Managers.Scene_Manager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Menu
{
    [RequireComponent(typeof(Button))]
    public class OpenSceneButton : OnClickButton
    {
        [SerializeField] private SceneType targetScene;
        
        [Inject] private MySceneManager _mySceneManager;

        protected override void OnClick()
        {
            _mySceneManager.OpenScene(targetScene);
        }
    }
}
