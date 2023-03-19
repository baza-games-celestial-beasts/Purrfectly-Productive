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
        [SerializeField] private float minBreakTime = 5;
        [SerializeField] private float maxBreakTime = 15;
        [Space(10)]
        [SerializeField] private int brokenItemsToStop;
        [SerializeField] private int brokenItemsToLose;

        private GeneratorItem[] items;

        [ShowInInspector, ReadOnly]
        private readonly List<GeneratorItem> healthyItems = new();
        [ShowInInspector, ReadOnly]
        private List<GeneratorItem> brokenItems = new();

        [Inject] private GameStateManager _gameStateManager;
        
        public GeneratorItemState GeneratorState => BrokenItemsCount >= brokenItemsToStop
            ? GeneratorItemState.Broken
            : GeneratorItemState.Health;
        
        public event Action OnBroken;
        public event Action OnFixed;
        #endregion

        #region Properties
        public float GeneratorStopTime =>  (brokenItemsToLose - brokenItemsToStop);
        public float BrokenItemsToLose => brokenItemsToLose;
        private float LeftItemsToDeath => (brokenItemsToLose - BrokenItemsCount);

        public int BrokenItemsCount => brokenItems.Count;

        public float HpValue => LeftItemsToDeath / BrokenItemsToLose;
        
        [ShowInInspector, ReadOnly]
        private float BreakTime => Mathf.Lerp(minBreakTime, maxBreakTime, (1- HpValue));
        #endregion

        private void Start()
        {
            Init();
        }

        private void Init()
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

        private IEnumerator BreakItems()
        {
            while (true)
            {
                yield return new WaitForSeconds(BreakTime);
                BreakItem();
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private void BreakItem()
        {
            if (healthyItems.Count == 0)
                return;
            
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

            if (brokenItems.Count > brokenItemsToStop)
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
