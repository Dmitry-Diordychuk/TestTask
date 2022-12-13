using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class GameContext : SingletonBehaviour<GameContext>
    {
        private readonly List<object> _listeners = new();
        private readonly List<object> _services = new();

        public enum GameState
        {
            None,
            Init,
            Play,
            Finish
        }

        public GameState CurrentGameState => _currentGameState;
        
        private GameState _currentGameState = GameState.None;
        

        private void Awake()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IConstructListener constructListener)
                {
                    constructListener.Construct(this as GameContext);
                }
            }
        }

        public T GetService<T>()
        {
            foreach (var service in _services)
            {
                if (service is T desiredService)
                {
                    return desiredService;
                }
            }

            throw new Exception($"Services of type {typeof(T).Name} wasn't located!");
        }

        public void AddService(object service)
        {
            _services.Add(service);
        }

        public void AddListener(object listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(object listener)
        {
            _listeners.Remove(listener);
        }

        public void SetGameState(GameState state)
        {
            if (_currentGameState != state)
            {
                _currentGameState = state;
                foreach (var listener in _listeners)
                {
                    switch (state)
                    {
                        case GameState.Init:
                            if (listener is IGameInitListener initListener)
                            {
                                initListener.OnGameInit();
                            }
                            break;
                        case GameState.Play:
                            if (listener is IGamePlayListener playListener)
                            {
                                playListener.OnGamePlay();
                            }
                            break;
                        case GameState.Finish:
                            if (listener is IGameFinishListener finishListener)
                            {
                                finishListener.OnGameFinish();
                            }
                            break;
                    }
                }
            }
        }
    }

    public interface IGameInitListener
    {
        void OnGameInit();
    }

    public interface IGamePlayListener
    {
        void OnGamePlay();
    }

    public interface IGameFinishListener
    {
        void OnGameFinish();
    }

    public interface IConstructListener
    {
        void Construct(GameContext gameContext);
    }
}
