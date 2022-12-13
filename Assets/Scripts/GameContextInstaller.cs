using UnityEngine;

namespace TestTask
{
	public class GameContextInstaller : MonoBehaviour
	{
		[SerializeField] private GameContext _gameContext;
		[SerializeField] private MonoBehaviour[] listeners;
		[SerializeField] private MonoBehaviour[] services;

		private void Awake()
		{
			foreach (var listener in listeners)
			{
				_gameContext.AddListener(listener);
			}

			foreach (var service in services)
			{
				_gameContext.AddService(service);
			}
		}
	}
}