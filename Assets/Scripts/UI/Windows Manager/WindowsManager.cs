using System;
using System.Collections;
using DG.Tweening;
using UI;
using UnityEngine;

public class WindowsManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private CanvasGroup[] windows;
    [SerializeField] private WindowType currentWindow;

    private const float SwapDuration = 0.25f;
    #endregion

    private void Start()
    {
        
    }

    public void SwapWindow(WindowType window)
    {
        currentWindow = window;
        foreach (var item in windows)
        {
            CanvasGroupSwap(item, false);
        }

        CanvasGroupSwap(windows[(int) currentWindow], true);
    }
        
    public void SwapWindow(WindowType window, float delay)
    {
        StartCoroutine(SwapWithDelay(window, delay));
    }

    private IEnumerator SwapWithDelay(WindowType window, float delay)
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