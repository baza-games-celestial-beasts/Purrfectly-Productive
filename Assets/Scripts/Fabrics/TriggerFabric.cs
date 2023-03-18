using System;
using UnityEngine;

namespace Fabrics
{
    public class TriggerFabric: Fabric
    {
        private bool _onTrigger;

        private void Update()
        {
            if (_onTrigger && Input.GetKeyDown(KeyCode.F) && !IsBusy)
            {
                Create();
            }
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            _onTrigger = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _onTrigger = false;
        }
    }
}