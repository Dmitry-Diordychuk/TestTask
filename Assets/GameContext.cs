using System;
using UnityEngine;

namespace TestTask
{
    public class GameContext : MonoBehaviour
    {
        public event Action OnGameInit;
        public event Action OnGamePlay;
        public event Action OnGameFinish;

        public enum GameState
        {
            Init,
            Play,
            Finish
        }

        private GameState _currentGameState;

        public void SetGameState(GameState state)
        {
            if (_currentGameState != state)
            {
                _currentGameState = state;
                switch (state)
                {
                    case GameState.Init:
                        OnGameInit?.Invoke();
                        break;
                    case GameState.Play:
                        OnGamePlay?.Invoke();
                        break;
                    case GameState.Finish:
                        OnGameFinish?.Invoke();
                        break;
                }
            }
        }
        
        private void OnEnable()
        {
            OnGameInit?.Invoke();
        }

        private void OnDisable()
        {
            OnGameFinish?.Invoke();
        }
    }
}
