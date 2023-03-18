using System;
using NaughtyAttributes;
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
