using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
	public class PlayerHand : MonoBehaviour
	{
		[SerializeField] private List<Card> hand = new ();

		public void AddCard(Card card)
		{
			card.transform.SetParent(transform);
			hand.Add(card);
		}
	}
}