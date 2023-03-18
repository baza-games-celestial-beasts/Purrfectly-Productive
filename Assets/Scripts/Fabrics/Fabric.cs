using System;
using UnityEngine;
using Items;
using UnityEngine.Serialization;
using Zenject;

namespace Fabrics
{
    public class Fabric: MonoBehaviour
    {
        [SerializeField] protected ItemType inputItem;

        [SerializeField] protected bool isCraft;
        [SerializeField] protected int bufferMaxSize;
        [SerializeField] protected int bufferDefaultSize;
        private int _bufferSize;

        [SerializeField] private float delayTime;

        public bool IsBusy;

        [Inject] private Inventory _inventory;

        private SpawnComponent _spawner;

        private void Start()
        {
            _bufferSize = bufferDefaultSize;
        }

        public void Create()
        {
            bool defaultCreate = _bufferSize < bufferMaxSize;
            bool create = defaultCreate;
            if (isCraft)
            {
                create = false;
                if (_inventory.CheckPutItem(inputItem))
                {
                    create = defaultCreate;
                }
            }

            if (create)
            {
                if (isCraft)
                {
                    _inventory.PutItem(inputItem);
                }
                
                _bufferSize = Math.Max(_bufferSize + 1, bufferMaxSize);
                IsBusy = true;
                Invoke(nameof(Spawn), delayTime);
            }
        }

        private void Spawn()
        {
            _spawner.Spawn();
            IsBusy = false;
        }
    }
}