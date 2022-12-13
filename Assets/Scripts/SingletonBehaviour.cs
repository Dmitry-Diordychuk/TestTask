using UnityEngine;

namespace TestTask
{
	public class SingletonBehaviour<T> : MonoBehaviour where T: Component
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<T>();

					if (_instance == null)
					{
						GameObject obj = new GameObject(typeof(T).Name);
						_instance = obj.AddComponent<T>();
					}
				}

				return _instance;
			}
		}

		private void Awake()
		{
			if (_instance != null)
			{
				Destroy(_instance);
			}
			else
			{
				_instance = this as T;
				DontDestroyOnLoad(gameObject);
			}
		}
	}
}