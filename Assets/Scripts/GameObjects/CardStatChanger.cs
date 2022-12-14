using System.Linq;
using TestTask.UI;
using UnityEngine;

namespace TestTask.GameObjects
{
	public class CardStatChanger : MonoBehaviour, IGameStartListener
	{
		[SerializeField] private PlayerHand playerHand;
		[SerializeField] private ButtonBehaviour changeButton;

		private int _index;
		private bool _isGameStart;

		private void OnEnable()
		{
			changeButton.OnClick += OnClick;
		}

		private void OnDisable()
		{
			changeButton.OnClick -= OnClick;
		}
		
		void IGameStartListener.OnGameStart()
		{
			_isGameStart = true;
		}

		private void OnClick()
		{
			if (!_isGameStart || playerHand.GetCount() <= 0)
			{
				return;
			}

			_index %= playerHand.GetCount();

			if (playerHand.TryGetCardAt(_index, out var card))
			{
				switch (Random.Range(0, 3))
				{
					case 0:
						card.attack.Assign(GetNewRandomValue(card.attack.Value));
						break;
					case 1:
						card.hp.Assign(GetNewRandomValue(card.hp.Value));
						break;
					case 2:
						card.mana.Assign(GetNewRandomValue(card.mana.Value));
						break;
				}
			}

			_index++;
		}

		private int GetNewRandomValue(int currentValue)
		{
			int[] values = Enumerable.Range(-2, 10).Where(item => item != currentValue).ToArray();
			return values[Random.Range(0, values.Length)];
		}
	}
}