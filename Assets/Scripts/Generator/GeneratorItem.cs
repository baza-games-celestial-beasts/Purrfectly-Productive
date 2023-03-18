using Sirenix.OdinInspector;
using UnityEngine;

namespace Generator
{
    public class GeneratorItem : MonoBehaviour
    {
        [SerializeField, ReadOnly] 
        private GeneratorItemState currentState;

        private void Start()
        {
            
        }
    }
}
