using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class GameManager : MonoBehaviour
{	
	public static Transform m_trans;
	public static Transform m_scene_root;

	// Use this for initialization
	void Awake () {
		m_trans = transform;
	}

	void Start()
	{
		HOTween.Init();
		Singleton<AudioManager>.Instance.Init();
		Singleton<WindowManager>.Instance.Init();
		//Singleton<DataManager>.Instance.Init();
		Singleton<DialogManager>.Instance.Init();		
		Singleton<SceneManager>.Instance.HideScene();
		Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.title);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
