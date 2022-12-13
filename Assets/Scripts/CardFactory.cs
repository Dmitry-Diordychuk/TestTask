using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TestTask
{
    public class CardFactory : MonoBehaviour, IConstructListener
    {
        [SerializeField] private Card emptyCardPrefab;

        private ImageService _imageService;

        public void Construct(GameContext gameContext)
        {
            _imageService = gameContext.GetService<ImageService>();
        }

        public void OrderCard(CardData cardData, Action<Card> onReady)
        {
            Card card = Instantiate(emptyCardPrefab);
            card.Init(cardData);
            _imageService.GetImage(
                cardData.Title, 
                (Texture2D image) =>
                {
                    card.UpdateArt(image);
                    onReady(card);
                }, (string message) =>
                {
                    Debug.LogWarning($"Art downloading failed with error: ${message}", this);
                    onReady(card);
                }
            );
        }
    }
}
