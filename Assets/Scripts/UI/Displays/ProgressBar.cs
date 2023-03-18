using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Displays
{
    [RequireComponent(typeof(Slider))]
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private float sliderSpeed;
        
        private Slider _slider;
        
        
        protected void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        protected virtual void Start(){}

        
        protected void ChangeValue(float targetValue)
        {
            _slider.DOValue(targetValue, sliderSpeed).SetSpeedBased().SetEase(Ease.Linear);
        }

        protected void ChangeValueInstantly(float targetValue)
        {
            _slider.value = targetValue;
        }
    }
}
