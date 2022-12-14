using System.Collections.Generic;
using DG.Tweening;
using TestTask.Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace TestTask.GameObjects
{
	public class PlayerHand : MonoBehaviour, IConstructListener, IGameInitListener, IGameStartListener
	{
		[SerializeField] private float cardMovementTime;
		[SerializeField] private float cardsEllipseHeight;
		[SerializeField] private float cardsEllipseWidth;
		[SerializeField] private float cardsCenterOffset;
		[SerializeField] private float ellipseCenterYOffset;
		[SerializeField] private WastePile _wastePile;

		private readonly List<Card> _hand = new();

		private CardDispenser _cardDispenser;

		private float _ellipseBDivA;

		void IConstructListener.Construct(GameContext gameContext)
		{
			_cardDispenser = gameContext.GetService<CardDispenser>();
		}
		
		void IGameInitListener.OnGameInit()
		{
			_ellipseBDivA = cardsEllipseHeight / cardsEllipseWidth;
		}

		void IGameStartListener.OnGameStart()
		{
			for (int i = 0; i < Random.Range(4, 7); i++)
			{
				if (_cardDispenser.TryGetNextCard(out var card))
				{
					AddCard(card);
				}
			}

			OrderCards();
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

		public int GetCount()
		{
			return _hand.Count;
		}
		
		private void AddCard(Card card)
		{
			Transform cardTransform = card.transform;
			cardTransform.SetParent(transform);
			cardTransform.position = transform.position;
			card.OnDeath += OnCardDeath;
			_hand.Add(card);
		}

		private void OnCardDeath(Card card)
		{
			card.OnDeath -= OnCardDeath;
			Discard(card);
			OrderCards();
		}

		private void Discard(Card card)
		{
			card.transform.DOKill();
			_hand.Remove(card);
			card.transform.SetParent(_wastePile.transform);
			card.transform.DOLocalMove(Vector3.zero, cardMovementTime).SetLink(card.gameObject);
			Vector3 randomRotation = new Vector3(0.0f, 0.0f, Random.Range(-180.0f, -30.0f));
			card.transform.DOLocalRotate(randomRotation, cardMovementTime).SetRelative().SetLink(card.gameObject);
		}

		private void OrderCards()
		{
			float halfCount = _hand.Count / 2;
			bool isOdd = _hand.Count % 2 != 0;

			for (int i = 0; i < _hand.Count; i++)
			{
				float x = i * cardsCenterOffset - halfCount * cardsCenterOffset +
				          (isOdd ? 0.0f : cardsCenterOffset / 2.0f);
				var endPosition = new Vector3(
					x,
					EllipseFunc(x),
					_hand[i].transform.position.z
				);
				_hand[i].transform.DOLocalMove(endPosition, cardMovementTime, true).SetLink(_hand[i].gameObject);

				Vector3 lookPoint = new Vector3(0.0f, ellipseCenterYOffset, 0.0f);
				Quaternion rotation = Quaternion.LookRotation(lookPoint - endPosition, Vector3.forward);
				rotation.x = 0.0f;
				rotation.y = 0.0f;
				_hand[i].transform.rotation = rotation;
				_hand[i].transform.DOLocalRotateQuaternion(rotation, cardMovementTime).SetLink(_hand[i].gameObject)
					.SetLink(_hand[i].gameObject);
			}
		}

		private float EllipseFunc(float x)
		{
			return _ellipseBDivA * Mathf.Sqrt((cardsEllipseWidth - x) * (cardsEllipseWidth + x));
		}
	}
}