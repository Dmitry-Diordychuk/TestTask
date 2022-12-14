using UnityEngine;
using UnityEngine.UI;

namespace TestTask.UI
{
	public class Art : MonoBehaviour
	{
		private RawImage _image;

		private void Awake()
		{
			_image = GetComponent<RawImage>();
		}

		public void SetArt(Texture2D art)
		{
			_image.texture = art;
		}
	}
}