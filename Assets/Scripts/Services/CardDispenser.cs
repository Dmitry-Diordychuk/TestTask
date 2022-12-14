using System;
using System.Collections.Generic;
using TestTask.GameObjects;
using TestTask.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestTask.Services
{
	public class CardDispenser : MonoBehaviour, IConstructListener, IGameInitListener
	{
		[SerializeField] private CardData nullData;
		[SerializeField] private List<CardData> _cardDataCollection;
		[SerializeField] private Card emptyCardPrefab;

		[HideInInspector] public bool isInitialized;

		private ImageService _imageService;

		private int _orderReadyCounter;
		private int _lastCardIndex;
		private Card _nullCard;

		public void Construct(GameContext gameContext)
		{
			_imageService = gameContext.GetService<ImageService>();
		}

		private void Awake()
		{
			_nullCard = GetCard(nullData);
		}

		public void OnGameInit()
		{
			foreach (var data in _cardDataCollection)
			{
				OrderCardArt(data, () =>
				{
					_orderReadyCounter++;
					if (_cardDataCollection.Count == _orderReadyCounter)
					{
						isInitialized = true;
					}
				});
			}
		}

		public Card GetNullCard()
		{
			return _nullCard;
		}

		public Card GetCard(CardData cardData)
		{
			Card card = Instantiate(emptyCardPrefab);
			card.Data = cardData;
			return card;
		}

		public Card GetRandomCard()
		{
			return GetCard(_cardDataCollection[Random.Range(0, _cardDataCollection.Count)]);
		}

		public bool TryGetNextCard(out Card card)
		{
			if (_lastCardIndex < _cardDataCollection.Count)
			{
				card = GetCard(_cardDataCollection[_lastCardIndex]);
				_lastCardIndex++;
				return true;
			}

			card = GetNullCard();
			return false;
		}

		private void OrderCardArt(CardData data, Action onReady)
		{
			_imageService.GetImage(
				data.Title,
				(image) =>
				{
					data.Art = image;
					onReady();
				}, (message) =>
				{
					Debug.LogWarning($"Art downloading for {data.Title} failed with error: ${message}", this);
					onReady();
				}
			);
		}
	}
}