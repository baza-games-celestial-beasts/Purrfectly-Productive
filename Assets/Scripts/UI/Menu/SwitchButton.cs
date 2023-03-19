using UI.Windows_Manager;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace UI.Menu
{
    public class SwitchButton: OnClickButton
    {
        [SerializeField] private MenuWindows toMenu;
        
        [Inject] private WindowsManager _windowsManager;
        protected override void OnClick()
        {
            _windowsManager.SwapWindow(toMenu);
        }
    }
}