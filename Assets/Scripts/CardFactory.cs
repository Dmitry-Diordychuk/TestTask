using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TestTask
{
    public class CardFactory : MonoBehaviour, IConstructable
    {
        [SerializeField] private List<CardData> _cardDataCollection;
        [SerializeField] private Card cardPrefab;
        
        private ImageService _imageService;
        private List<Object> _cardPrefabs;

        public void Construct(GameContext gameContext)
        {
            _imageService = gameContext.GetService<ImageService>();
        }

        private void Awake()
        {
            foreach (var cardData in _cardDataCollection)
            {
                
            }
        }

        public Card CreateCard(CardData cardData)
        {
            Card card = Instantiate(cardPrefab);
            card.Init(cardData, _imageService.GetImage());
            return card;
        }
    }
}
