using System;
using UnityEngine;

namespace TestTask
{
	public class ValueWidgetAdapter : MonoBehaviour
	{
		[SerializeField] private ValueWidget widget;
		[SerializeField] private IntValue value;

		private void OnEnable()
		{
			value.OnValueChanged += OnValueChanged;
		}

		private void OnValueChanged(int newValue)
		{
			widget.UpdateValue(newValue);
		}
	}
}