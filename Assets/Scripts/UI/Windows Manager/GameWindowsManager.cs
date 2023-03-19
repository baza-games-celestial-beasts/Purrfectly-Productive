using DG.Tweening;
using Managers.Game_States;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace UI.Windows_Manager
{
    public class GameWindowsManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup[] windows;
        [ShowInInspector] private GameWindows windowTypes;

        [Inject] private GameStateManager _gameStateManager;
        
        private const float SwapDuration = 0.25f;
        
        private void Start()
        {
            _gameStateManager.Fail += () =>
            {
                OpenWindow(GameWindows.LoseScreen);
            };
            _gameStateManager.Victory += () =>
            {
                OpenWindow(GameWindows.WinScreen);
            };

            CloseAllWindows();
            OpenWindow(GameWindows.Game);
        }

        private void CloseAllWindows()
        {
            foreach (var window in windows)
            {
                CanvasGroupSwap(window, false);
            }
        }

        private void OpenWindow(GameWindows windowType)
        {
            CanvasGroupSwap(windows[(int) windowType], true);
        }

        private void CanvasGroupSwap(CanvasGroup canvasGroup, bool isEnabled)
        {
            if (canvasGroup == null)
                return;
            
            canvasGroup.DOFade(isEnabled? 1 : 0, SwapDuration);

            canvasGroup.interactable = isEnabled;
            canvasGroup.blocksRaycasts = isEnabled;
        }
    }
}