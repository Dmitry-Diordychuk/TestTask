using TestTask.Services;
using UnityEngine;

namespace TestTask
{
	public class GameManager : MonoBehaviour, IConstructListener
	{
		private GameContext _gameContext;
		private CardDispenser _cardDispenser;

		private int _requiredNumberOfCards;

		public void Construct(GameContext gameContext)
		{
			_gameContext = gameContext;
			_cardDispenser = gameContext.GetService<CardDispenser>();
		}

		void Awake()
		{
			_gameContext.SetGameState(GameContext.GameState.Init);
		}

		private void Update()
		{
			if (_gameContext.CurrentGameState == GameContext.GameState.Init && _cardDispenser.isInitialized)
			{
				_gameContext.SetGameState(GameContext.GameState.Start);
			}
		}
	}
}