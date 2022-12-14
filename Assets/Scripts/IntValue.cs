using System;
using UnityEngine;

namespace TestTask
{
    public class IntValue : MonoBehaviour
    {
        public event Action<int> OnValueChanged;

        public int Value => _value;
        [SerializeField] private int _value;

        public void Assign(int value)
        {
            Debug.Log($"Current value = {_value}; New value = {value}");
            if (_value != value)
            {
                OnValueChanged?.Invoke(value);
            }
            _value = value;
        }
        
        public void Add(int amount)
        {
            _value += amount;
            OnValueChanged?.Invoke(_value);
        }

        public void Sub(int amount)
        {
            _value -= amount;
            OnValueChanged?.Invoke(_value);
        }
    }
}
