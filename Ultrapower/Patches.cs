using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Networking;
using Logger = BepInEx.Logging.Logger;
using Object = UnityEngine.Object;

#pragma warning disable IDE0060
namespace Ultrapower;

public static class DeliverDamagePatch
{

	//public void DeliverDamage(GameObject target, Vector3 force, Vector3 hitPoint, float multiplier, bool tryForExplode, float critMultiplier = 0F, GameObject sourceWeapon = null, bool ignoreTotalDamageTakenMultiplier = false)

	[HarmonyPatch(typeof(EnemyIdentifier), nameof(EnemyIdentifier.DeliverDamage))]
	[HarmonyPrefix]
	public static void Prefix(EnemyIdentifier __instance, GameObject target, Vector3 force, Vector3 hitPoint,
	                          float multiplier, bool tryForExplode, float critMultiplier, GameObject sourceWeapon,
	                          bool ignoreTotalDamageTakenMultiplier)
	{

		Debug.Log($"butt {__instance}");

		try {
			// var player = MonoSingleton<NewMovement>.Instance.gameObject.GetComponent<AudioSource>();

			Debug.Log($"{Ultrapower._hit} {Ultrapower._src}");
			// var src = target.gameObject.AddComponent<AudioSource>();
			// src.clip = Ultrapower._hit;
			// src.name = "bottlecap";
			// src.tag  = "bottlecap";
			// src.Play();
			if (Ultrapower._src != null) {
				Ultrapower._src.Play();
			}
			// Object.Destroy(src);
			// var asss = new AudioSource() { clip = cl, name = "bottlecap"};
			// asss.Play();
		}
		catch (Exception e) {
			Debug.LogError(e);
		}
	}

}