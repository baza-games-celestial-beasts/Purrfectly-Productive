using Generator_Logic;
using UnityEngine;
using Zenject;

namespace UI.Displays
{
    public class GeneratorStateDisplay : ProgressBar
    {
        #region Variables
        [SerializeField] private RectTransform stopSign;
        
        [Inject] private Generator _generator;
        #endregion
        
        protected override void Start()
        {
            base.Start();
            ChangeValueInstantly(1f);

            SetStopSignPosition();
            
        }

        private void Update()
        {
            DisplayState();
        }

        private void DisplayState()
        {
            var targetValue = _generator.LeftTimeToDeath / _generator.FullTimeToDeath;
            ChangeValue(targetValue);
        }
        
        private void SetStopSignPosition()
        {
            var targetPosition = Vector3.zero;
            
            var maxPosition = GetComponent<RectTransform>().sizeDelta.x;
            var positionCoefficient = _generator.GeneratorStopTime / _generator.FullTimeToDeath;
            targetPosition.x = positionCoefficient * maxPosition;

            stopSign.localPosition += targetPosition;
        }
    }
}