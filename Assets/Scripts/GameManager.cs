using System;
using UnityEngine;

namespace TestTask
{
    public class GameManager : MonoBehaviour, IConstructable
    {
        private GameContext _gameContext;
        private CardFactory _cardFactory;

        public void Construct(GameContext gameContext)
        {
            _gameContext = gameContext;
            _cardFactory = gameContext.GetService<CardFactory>();
        }
        
        void Awake()
        {
            _gameContext.SetGameState(GameContext.GameState.Init);
            
            //int randomIndex = Mathf.RoundToInt(Random.value * (cardData.Count - 1));
            
            //_cardFactory.CreateCard()
        }

        private void Update()
        {
        }
    }
}
