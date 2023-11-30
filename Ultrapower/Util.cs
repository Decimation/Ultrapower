// $User.Name $File.ProjectName $File.FileName
// $File.CreatedYear-$File.CreatedMonth-$File.CreatedDay @ $File.CreatedHour:$File.CreatedMinute

using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Ultrapower;

public static class Util
{

	public static AudioClip LoadClip(string path)
	{
		Debug.Log(path);
		AudioClip clip = null;

		using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV)) {
			uwr.SendWebRequest();

			// wrap tasks in try/catch, otherwise it'll fail silently
			try {
				while (!uwr.isDone) 
					_ = 0;

				if (uwr.isNetworkError || uwr.isHttpError) Debug.Log($"{uwr.error}");
				else {
					clip = DownloadHandlerAudioClip.GetContent(uwr);
				}
			}
			catch (Exception err) {
				Debug.Log($"{err.Message}, {err.StackTrace}");
			}
		}

		return clip;
	}

}