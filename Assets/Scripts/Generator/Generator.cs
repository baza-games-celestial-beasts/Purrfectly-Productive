using UnityEngine;

namespace Generator
{
    public class Generator : MonoBehaviour
    {
        #region Variables
        private GeneratorItem[] items;
        #endregion


        #region Monobehaviour Callbacks
        private void Start()
        {
            items = GetComponentsInChildren<GeneratorItem>();
        }
        #endregion
    }
}
