using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsHandler
{
	// Singleton to avoid instatiating handlers unnecessarily
	private static Dictionary<string, SettingsHandler> instances;
	public static SettingsHandler GetInstance(string name)
	{
		if (instances == null)
			instances = new Dictionary<string, SettingsHandler>();
		if (!instances.ContainsKey(name))
			instances[name] = new SettingsHandler(name);
		return instances[name];
	}

	// Private, only to be accessed by GetInstance
	private SettingsHandler(string name)
	{
		path = Application.dataPath + "/" + name + ".con";
		keyValues = new Dictionary<string, string>();

		// Load in file contents
		try
		{
			StreamReader sr = new StreamReader(path);
			string contentStr = sr.ReadToEnd().Replace("\n", "").TrimEnd(';');
			string[] contentArr = contentStr.Split(';');
			sr.Close();

			// TODO: implement wrong format handling
			foreach(string setting in contentArr)
			{
				string[] settingKeyValue = setting.Split(':');
				string settingKey = settingKeyValue[0];
				string settingValue = settingKeyValue[1];
				keyValues[settingKey] = settingValue;
			}
		}
		catch(Exception e)
		{
			Debug.LogError(e.Message);
		}
	}

	private string path;
	private Dictionary<string, string> keyValues;

	public string GetString(string name)
	{
		name = "s_" + name;

		if (!keyValues.ContainsKey(name))
			return null; // TODO: replace
		return keyValues[name];
	}

	public void SetString(string name, string value)
	{
		name = "s_" + name;
		keyValues[name] = value;
	}

	public bool GetBool(string name)
	{
		name = "b_" + name;

		if (!keyValues.ContainsKey(name))
			return false; // TODO: replace
		return (keyValues[name] == "on") ? true : false;
	}

	public void SetBool(string name, bool value)
	{
		name = "b_" + name;
		keyValues[name] = (value) ? "on" : "off";
	}

	public int GetInt(string name)
	{
		name = "i_" + name;
		
		if (!keyValues.ContainsKey(name))
			return -1; // TODO: replace
		
		try
		{
			return Convert.ToInt32(keyValues[name]);
		}
		catch(FormatException e)
		{
			Debug.LogError(e.Message);
			return -1; // TODO: replace
		}
	}

	public void SetInt(string name, int value)
	{
		name = "i_" + name;
		keyValues[name] = value.ToString();
	}

	public float GetFloat(string name)
	{
		name = "f_" + name;
		
		if (!keyValues.ContainsKey(name))
			return -1f; // TODO: replace
		
		try
		{
			return Convert.ToSingle(keyValues[name]);
		}
		catch(FormatException e)
		{
			Debug.LogError(e.Message);
			return -1f; // TODO: replace
		}
	}

	public void SetFloat(string name, float value)
	{
		name = "f_" + name;
		keyValues[name] = value.ToString();
	}

	public bool Save()
	{
		try
		{
			StreamWriter sw = new StreamWriter(path);
			sw.Write(ToString());
			sw.Close();

			return true;
		}
		catch(Exception e)
		{	
			Debug.LogError(e.Message);
			return false;
		}
	}

	public override string ToString()
	{
		string returnValue = "";
		
		foreach(KeyValuePair<string, string> entry in keyValues)
		{
			returnValue += entry.Key + ":" + entry.Value + ";\n";
		}

		return returnValue;
	}
}

/*
b_subtitles:on;
s_language:english;
i_volume:2;
f_sensitivity:2.0;
*/