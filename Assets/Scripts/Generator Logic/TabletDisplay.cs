using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Generator_Logic
{
    public class TabletDisplay : MonoBehaviour
    {
        [SerializeField] private int brokenItemsToDisplay;
        [SerializeField] private UnityEvent fixedEvent;
        [SerializeField] private UnityEvent brokenEvent;
        
        [Inject] private Generator _generator;
        
        private void Start()
        {
            _generator.OnBroken += UpdateState;
            _generator.OnFixed += UpdateState;
        }

        private void UpdateState()
        {
            if (_generator.BrokenItemsCount >= brokenItemsToDisplay)
            {
                brokenEvent?.Invoke();
            }
            else
            {
                fixedEvent?.Invoke();   
            }
        }
    }
}