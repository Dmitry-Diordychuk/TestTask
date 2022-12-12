using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace TestTask
{
	public class ArtService : MonoBehaviour
	{
		[SerializeField] private string url;

		public IEnumerable Get()
		{
			using UnityWebRequest webRequest = UnityWebRequest.Get(url);
			yield return webRequest.SendWebRequest();

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
			Texture texture = DownloadHandlerTexture.GetContent(webRequest);
			yield return texture;
		}
	}
}