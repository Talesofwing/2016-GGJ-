using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using UnityEngine.UI;

public class EndingWindow : WindowBase {
	public Transform m_content_text_tran;
	public Text m_content_text;
	public static readonly int ENDING_TIME = 40;
	void Awake()
	{
		Messenger.AddListener("ending_show", ShowEnding);
	}

	public void ShowEnding()
	{
		Active(true);
		m_content_text.text = Singleton<SceneManager>.Instance.m_diary_builder.ToString();
		HOTween.To(m_content_text_tran, ENDING_TIME, "position", m_content_text_tran.position + new Vector3(0, 1000, 0));
	}
}
