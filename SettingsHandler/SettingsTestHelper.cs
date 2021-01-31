using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsTestHelper : MonoBehaviour
{
	[SerializeField] private string settingsName;
	private SettingsHandler settingsHandler;

	private void Start()
	{
		settingsHandler = SettingsHandler.GetInstance(settingsName);

		Debug.Log(settingsHandler.GetString("string"));
		Debug.Log(settingsHandler.GetBool("bool"));
		Debug.Log(settingsHandler.GetInt("int"));
		Debug.Log(settingsHandler.GetFloat("float"));
		
		settingsHandler.SetString("string", "string");
		settingsHandler.SetBool("bool", true);
		settingsHandler.SetInt("int", 10);
		settingsHandler.SetFloat("float", 10.2f);	

		Debug.Log(settingsHandler.GetString("string"));
		Debug.Log(settingsHandler.GetBool("bool"));
		Debug.Log(settingsHandler.GetInt("int"));
		Debug.Log(settingsHandler.GetFloat("float"));

		settingsHandler.Save();
	}
}
