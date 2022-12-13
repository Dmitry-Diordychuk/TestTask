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

        public void CreateCard(CardData cardData, Action<Card> onSuccess, Action<Card> onError)
        {
            Card card = Instantiate(emptyCardPrefab);
            card.Init(cardData);
            _imageService.GetImage(
                cardData.Title, 
                (Texture2D image) =>
                {
                    card.UpdateArt(image);
                    onSuccess(card);
                }, (string message) =>
                {
                    Debug.LogWarning($"Art downloading failed with error: ${message}", this);
                    onError(card);
                }
            );
        }
    }
}
