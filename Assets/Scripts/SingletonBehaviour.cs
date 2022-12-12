using System;
using UnityEngine;

namespace TestTask
{
	public class SingletonBehaviour<T> : MonoBehaviour
	{
		private static SingletonBehaviour<T> _singletonBehaviour;
        
		private void OnEnable()
		{
			if (_singletonBehaviour != null)
			{
				SingletonBehaviour<T> temp = _singletonBehaviour;
				_singletonBehaviour = this;
				Destroy(temp);
			}
			else
			{
				_singletonBehaviour = this;
			}
		}

		private void OnDisable()
		{
			Destroy(this);
		}
	}
}