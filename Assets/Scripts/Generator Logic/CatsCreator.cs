using System;
using System.Collections;
using Managers.Game_States;
using UnityEngine;
using Zenject;

namespace Generator_Logic
{
    public class CatsCreator : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private GameObject cat;
        [SerializeField] private float spawnTime;
        [SerializeField] private float targetCount;

        public float TargetCount => targetCount;
        public int CreatedCats {get; private set;}

        private Coroutine creatingCoroutine;

        [Inject] private GameStateManager _gameStateManager;
        [Inject] private Generator _generator;
        [Inject] private DiContainer _diContainer;

        public event Action OnCreatedCat;
        #endregion
        
        private void Start()
        {
            _gameStateManager.Finish += TryStopCreating;
            _generator.OnBroken += TryStopCreating;
            _generator.OnFix += StartCreatingCats;
            StartCreatingCats();
        }

        private IEnumerator CreateCats()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnTime);
                CreateCat();
            }
        }


        private void CreateCat()
        {
            var createdCat = _diContainer.InstantiatePrefab(cat, spawnTransform);
            CreatedCats++;
            
            OnCreatedCat?.Invoke();

            if (CreatedCats >= TargetCount)
            {
                _gameStateManager.ChangeState(GameState.Victory);
            }
        }

        private void StartCreatingCats()
        {
            TryStopCreating();
            
            creatingCoroutine = StartCoroutine(CreateCats());
        }
        
        private void TryStopCreating()
        {
            if (creatingCoroutine != null)
                StopCoroutine(creatingCoroutine);
        }
    }
}
