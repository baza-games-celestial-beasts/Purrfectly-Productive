using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace UI.Windows_Manager
{
    public class WindowsManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private CanvasGroup[] windows;
        [SerializeField] private MenuWindows currentWindow;

        private const float SwapDuration = 0.25f;
        #endregion

        public void SwapWindow(MenuWindows window)
        {
            currentWindow = window;
            foreach (var item in windows)
            {
                CanvasGroupSwap(item, false);
            }

            CanvasGroupSwap(windows[(int) currentWindow], true);
        }
        
        public void SwapWindow(MenuWindows window, float delay)
        {
            StartCoroutine(SwapWithDelay(window, delay));
        }

        private IEnumerator SwapWithDelay(MenuWindows window, float delay)
        {
            currentWindow = window;
            foreach (var item in windows)
            {
                CanvasGroupSwap(item, false);
            }

            yield return new WaitForSecondsRealtime(delay);
            CanvasGroupSwap(windows[(int) currentWindow], true);
        }
        
        public static void CanvasGroupSwap(CanvasGroup canvasGroup, bool isEnabled)
        {
            canvasGroup.DOFade(isEnabled? 1 : 0, SwapDuration);

            canvasGroup.interactable = isEnabled;
            canvasGroup.blocksRaycasts = isEnabled;
        }
    }
}