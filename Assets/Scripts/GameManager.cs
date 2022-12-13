using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class GameManager : MonoBehaviour, IConstructListener
    {
        [SerializeField] private List<CardData> cardCollection = new ();
        [SerializeField] private PlayerHand _playerHand;

        private GameContext _gameContext;
        private CardFactory _cardFactory;

        private readonly List<Card> _cards = new ();
        private int _numberOfCards;

        public void Construct(GameContext gameContext)
        {
            _gameContext = gameContext;
            _cardFactory = gameContext.GetService<CardFactory>();
        }
        
        void Awake()
        {
            _gameContext.SetGameState(GameContext.GameState.Init);

            _numberOfCards = Random.Range(4, 6);
            for (int i = 0; i < _numberOfCards; i++)
            {
                //int randomCardIndex = Mathf.RoundToInt(Random.value * (cardCollection.Count - 1));
                
                _cardFactory.CreateCard(cardCollection[i], (Card card) =>
                {
                    _cards.Add(card);
                }, (Card card) =>
                {
                    _cards.Add(card);
                });
                
            }
        }

        private void Update()
        {
            if (_gameContext.CurrentGameState == GameContext.GameState.Init && IsCardsReady)
            {
                Debug.Log("Here");
                int i = 0;
                foreach (var card in _cards)
                {
                    card.transform.position = new Vector3(i * 100.0f, 0.0f, 0.0f);
                    card.transform.SetParent(_playerHand.transform.parent);
                    _playerHand.AddCard(card);
                    i++;
                }
                _gameContext.SetGameState(GameContext.GameState.Play);
            }
        }

        private bool IsCardsReady => _cards.Count == _numberOfCards;
        
    }
}
