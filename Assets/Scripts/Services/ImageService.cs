using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace TestTask.Services
{
	public class ImageService : MonoBehaviour
	{
		[SerializeField] private string url;

		private readonly Dictionary<string, Texture2D> _alreadyDownloadedArt = new();

		public void GetImage(string imageName, Action<Texture2D> onSuccess, Action<string> onError)
		{
			if (_alreadyDownloadedArt.ContainsKey(imageName))
			{
				onSuccess(_alreadyDownloadedArt[imageName]);
				return;
			}

			StartCoroutine(DownloadImage(texture2D =>
			{
				Texture2D copy = new Texture2D(texture2D.width, texture2D.height);
				copy.SetPixels(texture2D.GetPixels());
				copy.Apply();
				_alreadyDownloadedArt[imageName] = copy;
				onSuccess(copy);
			}, onError));
		}

		private IEnumerator DownloadImage(Action<Texture2D> onSuccess, Action<string> onError)
		{
			using UnityWebRequest webRequest = UnityWebRequest.Get(url);
			webRequest.downloadHandler = new DownloadHandlerTexture();
			yield return webRequest.SendWebRequest();

			switch (webRequest.result)
			{
				case UnityWebRequest.Result.ConnectionError:
				case UnityWebRequest.Result.DataProcessingError:
					onError(webRequest.error);
					yield break;
				case UnityWebRequest.Result.ProtocolError:
					onError(webRequest.error);
					yield break;
			}

			Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);

			onSuccess(texture);
		}
	}
}