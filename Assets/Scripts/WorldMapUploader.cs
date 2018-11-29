using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WorldMapUploader : MonoBehaviour {
	static Uri BASE = new Uri("http://172.28.21.218:3000/");

	public IEnumerator Upload(byte[] worldMapData, Action<string> callback)
	{
		var request = new UnityWebRequest(new Uri(BASE, "upload").ToString());
		request.method = "POST";
		request.SetRequestHeader("Content-Type", "application/octet-stream");
		request.uploadHandler = new UploadHandlerRaw(worldMapData);
		var downloadhandler = new DownloadHandlerBuffer();
        request.downloadHandler = downloadhandler;
		yield return request.SendWebRequest();
		callback.Invoke(downloadhandler.text);
		yield return downloadhandler.text;
	}

	public IEnumerator Download(string path, Action<byte[]> callback)
	{
		var request = new UnityWebRequest(new Uri(BASE, path).ToString());
		var downloadhandler = new DownloadHandlerBuffer();
        request.downloadHandler = downloadhandler;
		yield return request.SendWebRequest();
		callback.Invoke(downloadhandler.data);
		yield return downloadhandler.data;
	}
}
