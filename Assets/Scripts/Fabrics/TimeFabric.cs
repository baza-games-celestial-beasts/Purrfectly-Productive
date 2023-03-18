using System;
using System.Text;
using UnityEngine;

namespace Fabrics
{
    public class TimeFabric: Fabric
    {
        [SerializeField] private float harvestTime;
        private float _elapsedTime;

        private void Update()
        {
            _elapsedTime += Time.deltaTime;

            int count = (int)(_elapsedTime / harvestTime);
            _elapsedTime -= harvestTime * Math.Max(count, 1);

            if (count > 0)
            {
                Create();
            }
        }
    }
}