using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Generator
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


        [Inject] private DiContainer _diContainer;

        public event Action OnCreatedCat;
        #endregion
        
        private void Start()
        {
            StartCoroutine(CreateCats());
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
        }
        
    }
}
