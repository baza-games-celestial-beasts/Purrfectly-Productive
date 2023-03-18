using System;
using UnityEngine;

namespace Generator_Logic
{
    public class Generator : MonoBehaviour
    {
        #region Variables
        private GeneratorItem[] items;

        public event Action OnBroken;
        public event Action OnFix;
        #endregion

        #region Monobehaviour Callbacks
        private void Start()
        {
            items = GetComponentsInChildren<GeneratorItem>();
        }
        #endregion
    }
}
