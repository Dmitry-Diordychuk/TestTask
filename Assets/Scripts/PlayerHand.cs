using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace TestTask
{
	public class PlayerHand : MonoBehaviour, IGameInitListener, IGamePlayListener
	{
		[SerializeField] private List<Card> hand = new ();
		[SerializeField] private float cardsEllipseHeight;
		[SerializeField] private float cardsEllipseWidth;
		[SerializeField] private float cardsCenterOffset;
		[SerializeField] private float handPositionYOffset;
		
		private float _ellipseBDivA;

		public void AddCard(Card card)
		{
			card.transform.SetParent(transform);
			card.transform.position = transform.position;
			hand.Add(card);
		}

		public void OnGameInit()
		{
			_ellipseBDivA = cardsEllipseHeight / cardsEllipseWidth;
		}
		
		public void OnGamePlay()
		{
			float halfCount = hand.Count / 2;
			bool isOdd = hand.Count % 2 != 0;

			for (int i = 0; i < hand.Count; i++)
			{
				float x = hand[i].transform.position.x + i * cardsCenterOffset - halfCount * cardsCenterOffset + (isOdd ? 0.0f : cardsCenterOffset / 2.0f);
				hand[i].transform.position = new Vector3(
					x,
					EllipseFunc(x - transform.position.x),
					hand[i].transform.position.z);
				Vector3 lookPoint = transform.position;
				lookPoint.y = handPositionYOffset;
				Quaternion rotation = Quaternion.LookRotation(lookPoint - hand[i].transform.position, Vector3.forward);
				rotation.x = 0.0f;
				rotation.y = 0.0f;
				hand[i].transform.rotation = rotation;

			}
		}

		private float EllipseFunc(float x)
		{
			return _ellipseBDivA * Mathf.Sqrt((cardsEllipseWidth - x) * (cardsEllipseWidth + x));
		}
	}
}