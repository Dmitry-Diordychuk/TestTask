using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestTask
{
    public class CardDispenser : MonoBehaviour, IConstructListener, IGameInitListener
    {
        [SerializeField] private CardData nullData;
        [SerializeField] private List<CardData> _cardDataCollection;
        [SerializeField] private Card emptyCardPrefab;

        public int _lastCardIndex;
        [HideInInspector] public bool isInitialized;
        
        private ImageService _imageService;

        private int _orderReadyCounter;

        public void Construct(GameContext gameContext)
        {
            _imageService = gameContext.GetService<ImageService>();
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
            return GetCard(nullData);
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

        public void OrderCardArt(CardData data, Action onReady)
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
