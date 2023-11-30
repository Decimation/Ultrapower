// $User.Name $File.ProjectName $File.FileName
// $File.CreatedYear-$File.CreatedMonth-$File.CreatedDay @ $File.CreatedHour:$File.CreatedMinute

using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace Ultrapower;

[BepInPlugin(GUID, Name, Version)]
[BepInProcess("ULTRAKILL.exe")]
public class Ultrapower : BaseUnityPlugin
{

	public const string GUID    = "decimation.ultrakill.ultrapower";
	public const string Name    = "Ultrapower";
	public const string Version = "1.0.0";

	private static Harmony harmony;

	internal static AudioClip _hit;
	internal static AudioSource _src;

	public void Start()
	{
		harmony = new Harmony(GUID);
		harmony.PatchAll(typeof(DeliverDamagePatch));

		Debug.Log($"{Name} v{Version} has started.");
		Logger.LogInfo($"{Name} v{Version} has started.");

		_hit        = Util.LoadClip("file://" + @"C:\Users\Deci\VSProjects\Ultrapower\Ultrapower\bottlecap.wav");
		_src        = gameObject.AddComponent<AudioSource>();
		_src.clip   = _hit;
		_src.volume = 0.6f;
		// _src.tag  = "bottlecap";
		_src.name="bottlecap";
	}

	public void OnDestroy() { }

}