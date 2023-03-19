using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Editor
{
    public class SpeedUpTime : MonoBehaviour
    {
        [SerializeField, ShowIf(nameof(IsPlaying))] private float timeScale = 1f;

        private bool IsPlaying => Application.isEditor && Application.isPlaying;
        
        private void OnValidate()
        {
            Time.timeScale = timeScale;
        }
    }
}