using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace TestTask
{
	public class PlayerHand : MonoBehaviour, IConstructListener, IGameInitListener, IGameStartListener
	{
		[SerializeField] private float cardsEllipseHeight;
		[SerializeField] private float cardsEllipseWidth;
		[SerializeField] private float cardsCenterOffset;
		[SerializeField] private float handPositionYOffset;

		private readonly List<Card> _hand = new ();
		
		private CardDispenser _cardDispenser;
		
		private float _ellipseBDivA;

		public void Construct(GameContext gameContext)
		{
			_cardDispenser = gameContext.GetService<CardDispenser>();
		}

		public void AddCard(Card card)
		{
			card.transform.SetParent(transform);
			card.transform.position = transform.position;
			_hand.Add(card);
		}

		public bool TryGetCardAt(int index, out Card card)
		{
			if (index < _hand.Count)
			{
				card = _hand[index];
				return true;
			}

			card = _cardDispenser.GetNullCard();
			return false;
		}

		public void OnGameInit()
		{
			_ellipseBDivA = cardsEllipseHeight / cardsEllipseWidth;
		}

		public void OnGamePlay()
		{
			for (int i = 0; i < Random.Range(4, 7); i++)
			{
				if (_cardDispenser.TryGetNextCard(out var card))
				{
					AddCard(card);
				}
			}
			
			float halfCount = _hand.Count / 2;
			bool isOdd = _hand.Count % 2 != 0;

			for (int i = 0; i < _hand.Count; i++)
			{
				float x = _hand[i].transform.position.x + i * cardsCenterOffset - halfCount * cardsCenterOffset + (isOdd ? 0.0f : cardsCenterOffset / 2.0f);
				_hand[i].transform.position = new Vector3(
					x,
					EllipseFunc(x - transform.position.x),
					_hand[i].transform.position.z);
				Vector3 lookPoint = transform.position;
				lookPoint.y = handPositionYOffset;
				Quaternion rotation = Quaternion.LookRotation(lookPoint - _hand[i].transform.position, Vector3.forward);
				rotation.x = 0.0f;
				rotation.y = 0.0f;
				_hand[i].transform.rotation = rotation;

			}
		}

		private float EllipseFunc(float x)
		{
			return _ellipseBDivA * Mathf.Sqrt((cardsEllipseWidth - x) * (cardsEllipseWidth + x));
		}
	}
}