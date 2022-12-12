using System;
using UnityEngine;

namespace TestTask
{
	public class GameContextInstaller : MonoBehaviour
	{
		[SerializeField] private GameContext _gameContext;
		[SerializeField] private MonoBehaviour[] listeners;

		private void Awake()
		{
			foreach (var listener in listeners)
			{
				_gameContext.AddListener(listener);
			}
		}
	}
}