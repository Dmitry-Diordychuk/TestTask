using System;
using UnityEngine;

namespace TestTask.Mechanics
{
	public class IntValue : MonoBehaviour
	{
		public event Action<int> OnValueChanged;

		public int Value => _value;
		[SerializeField] private int _value;

		public void Assign(int value)
		{
			if (_value != value)
			{
				OnValueChanged?.Invoke(value);
			}

			_value = value;
		}
	}
}