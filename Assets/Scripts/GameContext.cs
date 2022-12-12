using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TestTask
{
    public class GameContext : SingletonBehaviour<GameContext>
    {
        private readonly List<object> _listeners = new();
        private readonly List<object> _services = new();
        
        public enum GameState
        {
            Init,
            Play,
            Finish
        }

        private GameState _currentGameState;

        private void Awake()
        {
            foreach (var service in _services)
            {
                if (service is IConstructable constructable)
                {
                    constructable.Construct(this as GameContext);
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

    public interface IConstructable
    {
        void Construct(GameContext gameContext);
    }
}
