using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace TestTask.UI
{
	public class ValueWidget : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI valueText;

		public void UpdateValue(int value)
		{
			StartCoroutine(CountAnimation(value));
		}

		private IEnumerator CountAnimation(int value)
		{
			int currentValue = Int32.Parse(valueText.text);
			int one = currentValue < value ? +1 : -1;

			for (int i = currentValue + one; i != value + one;)
			{
				yield return new WaitForSeconds(0.1f);

				valueText.text = i.ToString();

				i += one;
			}
		}
	}
}