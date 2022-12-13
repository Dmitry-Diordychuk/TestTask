using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class GameManager : MonoBehaviour, IConstructListener
    {
        [SerializeField] private List<CardData> cardCollection = new ();
        [SerializeField] private PlayerHand _playerHand;

        private GameContext _gameContext;
        private CardFactory _cardDispenser;

        private readonly List<Card> _cards = new ();
        private int _requiredNumberOfCards;

        public void Construct(GameContext gameContext)
        {
            _gameContext = gameContext;
            _cardDispenser = gameContext.GetService<CardFactory>();
        }
        
        void Awake()
        {
            _gameContext.SetGameState(GameContext.GameState.Init);

            _requiredNumberOfCards = Random.Range(4, 7);
            for (int i = 0; i < _requiredNumberOfCards; i++)
            {
                _cardDispenser.OrderCard(cardCollection[i], (Card card) =>
                {
                    _cards.Add(card);
                });
            }
        }

        private void Update()
        {
            if (_gameContext.CurrentGameState == GameContext.GameState.Init && IsCardsReady)
            {
                int i = 0;
                foreach (var card in _cards)
                {
                    _playerHand.AddCard(card);
                    i++;
                }
                _gameContext.SetGameState(GameContext.GameState.Start);
            }
            
            
        }

        private bool IsCardsReady => _cards.Count == _requiredNumberOfCards;
    }
}
