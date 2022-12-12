using TMPro;
using UnityEngine;

namespace TestTask
{
	public class ValueWidget : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI valueText;

		public void UpdateValue(int value)
		{
			valueText.text = value.ToString();
		}
	}
}