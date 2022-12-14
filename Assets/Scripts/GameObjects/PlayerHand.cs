using System.Collections.Generic;
using DG.Tweening;
using TestTask.Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace TestTask.GameObjects
{
	public class PlayerHand : MonoBehaviour, IConstructListener, IGameInitListener, IGameStartListener
	{
		[SerializeField] private float cardsEllipseHeight;
		[SerializeField] private float cardsEllipseWidth;
		[SerializeField] private float cardsCenterOffset;
		[SerializeField] private float arcCenterYOffset;
		[SerializeField] private WastePile _wastePile;

		private readonly List<Card> _hand = new();

		private CardDispenser _cardDispenser;

		private float _ellipseBDivA;

		public void Construct(GameContext gameContext)
		{
			_cardDispenser = gameContext.GetService<CardDispenser>();
		}

		public void AddCard(Card card)
		{
			Transform cardTransform = card.transform;
			cardTransform.SetParent(transform);
			cardTransform.position = transform.position;
			card.OnDeath += OnCardDeath;
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

		public int GetCount()
		{
			return _hand.Count;
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

			OrderCards();
		}

		private void OnCardDeath(Card card)
		{
			card.OnDeath -= OnCardDeath;
			Discard(card);
			OrderCards();
		}

		private void Discard(Card card)
		{
			_hand.Remove(card);
			card.transform.SetParent(_wastePile.transform);
			card.transform.DOMove(_wastePile.transform.position, 3.0f).SetLink(card.gameObject);
			Vector3 randomRotation = new Vector3(0.0f, 0.0f, Random.Range(-180.0f, -30.0f));
			card.transform.DOLocalRotate(randomRotation, 3.0f).SetLink(card.gameObject);
		}

		private void OrderCards()
		{
			float halfCount = _hand.Count / 2;
			bool isOdd = _hand.Count % 2 != 0;

			for (int i = 0; i < _hand.Count; i++)
			{
				var handPosition = transform.position;
				float x = handPosition.x + i * cardsCenterOffset - halfCount * cardsCenterOffset +
				          (isOdd ? 0.0f : cardsCenterOffset / 2.0f);
				var endPosition = new Vector3(
					x,
					EllipseFunc(x - handPosition.x),
					_hand[i].transform.position.z
				);
				_hand[i].transform.DOMove(endPosition, 3.0f, true).SetLink(_hand[i].gameObject);

				Vector3 lookPoint = handPosition;
				lookPoint.y += arcCenterYOffset;
				Quaternion rotation = Quaternion.LookRotation(lookPoint - endPosition, Vector3.forward);
				rotation.x = 0.0f;
				rotation.y = 0.0f;
				_hand[i].transform.rotation = rotation;
				_hand[i].transform.DORotateQuaternion(rotation, 3.0f).SetLink(_hand[i].gameObject)
					.SetLink(_hand[i].gameObject);
			}
		}

		private float EllipseFunc(float x)
		{
			return _ellipseBDivA * Mathf.Sqrt((cardsEllipseWidth - x) * (cardsEllipseWidth + x));
		}
	}
}