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
        public event Action OnFixed;
        #endregion

        #region Monobehaviour Callbacks
        private void Start()
        {
            items = GetComponentsInChildren<GeneratorItem>();
            foreach (var item in items)
            {
                item.OnFixed += FixItem;
            }
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
            
            targetItem.ChangeState(GeneratorItemState.Broken);
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

        private void FixItem(GeneratorItem targetItem)
        {
            brokenItems.Remove(targetItem);
            healthyItems.Add(targetItem);
            
            if (brokenItems.Count == brokenItemsToStop - 1)
            {
                OnFixed?.Invoke();
            }
        }
    }
}
