using Managers.Scene_Manager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Menu
{
    [RequireComponent(typeof(Button))]
    public class OpenGameButton : OnClickButton
    {
        [Inject] private MySceneManager _mySceneManager;
        
        protected override void OnClick()
        {
            _mySceneManager.OpenScene(SceneType.Game);
        }
    }
}
