using System;
using System.Collections;
using System.Collections.Generic;
using Managers.Game_States;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Generator_Logic
{
    public class Generator : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float breakTime;
        [Space(10)]
        [SerializeField] private int brokenItemsToStop;
        [SerializeField] private int brokenItemsToLose;

        private GeneratorItem[] items;

        [ShowInInspector, ReadOnly]
        private readonly List<GeneratorItem> healthyItems = new();
        [ShowInInspector, ReadOnly]
        private List<GeneratorItem> brokenItems = new();

        [Inject] private GameStateManager _gameStateManager;
        
        public event Action OnBroken;
        public event Action OnFix;
        #endregion

        #region Monobehaviour Callbacks
        private void Start()
        {
            items = GetComponentsInChildren<GeneratorItem>();
            healthyItems.AddRange(items);

            StartCoroutine(BreakItems());

            _gameStateManager.Finish += StopAllCoroutines;
        }
        #endregion

        private IEnumerator BreakItems()
        {
            while (true)
            {
                yield return new WaitForSeconds(breakTime);
                BreakItem();
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private void BreakItem()
        {
            var targetIndex = Random.Range(0, healthyItems.Count);
            var targetItem = healthyItems[targetIndex];
            healthyItems.RemoveAt(targetIndex);
            
            brokenItems.Add(targetItem);

            if (brokenItems.Count >= brokenItemsToLose)
            {
                _gameStateManager.ChangeState(GameState.Fail);
                return;
            }

            if (brokenItems.Count >= brokenItemsToStop)
            {
                OnBroken?.Invoke();
            }
        }
    }
}
