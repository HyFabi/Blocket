using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used for Global (Unique) Variables/Settings
/// <br></br>
/// <b>Use it gently!</b>
/// </summary>
public static class GlobalVariables
{
	public static readonly ushort maxItemCountForMultiple = 128;
	public static bool gameRunning;

	public static GameObject localPlayer;
	public static readonly List<GameObject> players = new List<GameObject>();
	public static bool isMultiplayer;

	#region Debug Variables
	public static readonly bool itemSlotButtonPressedLog = false;
	public static readonly bool itemTest = false;
	#endregion

	#region Setted Keys
	public static KeyCode openInventoryKey = KeyCode.E;
	public static KeyCode leftClick = KeyCode.Mouse0;
	public static KeyCode rightClick = KeyCode.Mouse1;
	#endregion
	public static int currentHealth { get; set; }
	public static int maxHealth { get; set; }
}
