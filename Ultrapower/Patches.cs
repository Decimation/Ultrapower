using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Networking;
using Logger = BepInEx.Logging.Logger;

namespace Ultrapower
{
	[BepInPlugin(GUID, Name, Version)]
	[BepInProcess("ULTRAKILL.exe")]
	public class Ultrapower : BaseUnityPlugin
	{

		public const   string  GUID    = "decimation.ultrakill.ultrapower";
		public const   string  Name    = "Ultrapower";
		public const   string  Version = "1.0.0";
		private static Harmony harmony;

		public void Start()
		{
			harmony = new Harmony(GUID);
			harmony.PatchAll(typeof(HitTweak));

			Debug.Log($"{Name} v{Version} has started.");
			Logger.LogInfo($"{Name} v{Version} has started.");
		}

		public void OnDestroy() { }

	}

	public static class HitTweak
	{

		static AudioClip LoadClip(string path)
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

		//public void DeliverDamage(GameObject target, Vector3 force, Vector3 hitPoint, float multiplier, bool tryForExplode, float critMultiplier = 0F, GameObject sourceWeapon = null, bool ignoreTotalDamageTakenMultiplier = false)

		[HarmonyPatch(typeof(EnemyIdentifier), nameof(EnemyIdentifier.DeliverDamage))]
		[HarmonyPrefix]
		public static void Prefix(EnemyIdentifier __instance, GameObject target, Vector3 force, Vector3 hitPoint,
		                          float multiplier, bool tryForExplode, float critMultiplier, GameObject sourceWeapon,
		                          bool ignoreTotalDamageTakenMultiplier)
		{

			Debug.Log($"butt {__instance}");

			try {
				var cl =  LoadClip("file://" + @"C:\Users\Deci\VSProjects\Ultrapower\Ultrapower\bottlecap.wav");

				Debug.Log($"{cl}");
				var src=target.gameObject.AddComponent<AudioSource>();
				src.clip = cl;
				src.name = "bottlecap";
				src.Play();
				// var asss = new AudioSource() { clip = cl, name = "bottlecap"};
				// asss.Play();
			}
			catch (Exception e) {
				Debug.LogError(e);
			}
		}

	}
}