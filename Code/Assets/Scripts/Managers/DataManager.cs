using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
public enum SCENE_TYPE
{
	MONTAIN,
	POLE,
	DACE_HALL,
	LAB,
	HOME,
	HOSPITAL,
	START,
	END,
	COUNT
}


public enum ACTION_TYPE
{
	EXPLORER,
	COUNT
}


public class DataManager : MonoBehaviour
{
	public static readonly string CONFIG_JSON = "Assets/Configs/config.json";
	public List<string> m_scene_list = new List<string>();
	public JSONObject m_data;

	public void Init()
	{
		//string content = File.ReadAllText(CONFIG_JSON);
		//m_data = new JSONObject(content);
	}
}

