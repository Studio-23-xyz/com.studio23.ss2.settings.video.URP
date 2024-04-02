
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio23.SS2.Settings.Video.Samples
{
    public class LoopedStepper : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _displayText;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private bool _makeUpdateImmedietly;
        [SerializeField] private int _selectedIndex;
        private IList _data;
        public UnityEvent<int> SelectedIndexUpdated;

        private void Start()
        {
            _leftButton.onClick.AddListener(DecreaseIndex);
            _rightButton.onClick.AddListener(IncreaseIndex);
        }

        public void InitializeData(IList data, int selectIndex = 0)
        {
            _selectedIndex = selectIndex;
            _data = data;
            SelectedIndexUpdateAction(false);
        }


        private void ShowText()
        {
            _displayText.text = _data[_selectedIndex].ToString();
        }

        public void IncreaseIndex()
        {
            if (_selectedIndex < _data.Count - 1)
            {
                _selectedIndex++;
            }
            else
            {
                _selectedIndex = 0;
            }
            SelectedIndexUpdateAction();
        }


        public void DecreaseIndex()
        {
            if (_selectedIndex > 0)
            {
                _selectedIndex--;
            }
            else
            {
                _selectedIndex = _data.Count - 1;
            }
            SelectedIndexUpdateAction();
        }

        public void SelectedIndexUpdateAction(bool checkLiveStatus = true)
        {
            ShowText();
            if(checkLiveStatus)
                if (!_makeUpdateImmedietly) return;
            ApplyAction();
        }

        private void ApplyAction()
        {
            SelectedIndexUpdated?.Invoke(_selectedIndex);
        }
    }
}
