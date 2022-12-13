using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace TestTask
{
	public class ImageService : MonoBehaviour
	{
		[SerializeField] private string url;

		public Texture2D GetImage()
		{
			using UnityWebRequest webRequest = UnityWebRequest.Get(url);
			webRequest.SendWebRequest();

			switch (webRequest.result)
			{
				case UnityWebRequest.Result.ConnectionError:
				case UnityWebRequest.Result.DataProcessingError:
					Debug.LogError("Error: " + webRequest.error, this);
					break;
				case UnityWebRequest.Result.ProtocolError:
					Debug.LogError("HTTP Error: " + webRequest.error, this);
					break;
			}
			
			webRequest.downloadHandler = new DownloadHandlerTexture();
			Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
			return texture;
		}
	}
}