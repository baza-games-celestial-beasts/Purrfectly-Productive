using Generator_Logic;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Displays
{
    public class ProgressDisplay : ProgressBar
    {
        [SerializeField] private TextMeshProUGUI progressText;

        [Inject] private CatsCreator _catsCreator;
        
        protected override void Start()
        {
            base.Start();
            ChangeValueInstantly(0f);

            _catsCreator.OnCreatedCat += UpdateValue;
            UpdateValue();
        }

        private void UpdateValue()
        {
            progressText.text = $"{_catsCreator.CreatedCats} / {_catsCreator.TargetCount}";
            var progress = _catsCreator.CreatedCats / _catsCreator.TargetCount;
            ChangeValue(progress);
        }
    }
}
