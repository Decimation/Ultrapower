using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;

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

		//public void DeliverDamage(GameObject target, Vector3 force, Vector3 hitPoint, float multiplier, bool tryForExplode, float critMultiplier = 0F, GameObject sourceWeapon = null, bool ignoreTotalDamageTakenMultiplier = false)

		[HarmonyPatch(typeof(EnemyIdentifier), nameof(EnemyIdentifier.DeliverDamage))]
		[HarmonyPrefix]
		public static void Prefix(EnemyIdentifier __instance, GameObject target, Vector3 force, Vector3 hitPoint,
		                          float multiplier, bool tryForExplode, float critMultiplier, GameObject sourceWeapon,
		                          bool ignoreTotalDamageTakenMultiplier)
		{
			Debug.Log($"butt {__instance}");
			
		}

	}
}