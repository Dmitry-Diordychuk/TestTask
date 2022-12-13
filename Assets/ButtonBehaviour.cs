using System;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask
{
    [RequireComponent(typeof(Button))]
    public class ButtonBehaviour : MonoBehaviour
    {
        public event Action OnClick;
        
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            OnClick?.Invoke();
        }
    }
}
