using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChooseWindow : WindowBase
{

	public List<Transform> m_choose_text_list = new List<Transform>();

	void Awake()
	{
		Messenger.AddListener<List<string>>("choose_show", SetChooseValue);
	}
	public void Choose0()
	{
		Messenger.Broadcast<int>("choose_chosen", 0);
		Active(false);
	}
	public void Choose1()
	{
		Messenger.Broadcast<int>("choose_chosen", 1);
		Active(false);
	}
	public void Choose2()
	{
		Messenger.Broadcast<int>("choose_chosen", 2);
		Active(false);
	}
	public void Choose3()
	{
		Messenger.Broadcast<int>("choose_chosen", 3);
		Active(false);
	}

	public void SetChooseValue(List<string> choose_name_list)
	{
		Active(true);
		for(int i = 0; i < Mathf.Min(choose_name_list.Count, 4); ++i)
		{
			Text choose_text = m_choose_text_list[i].GetComponent<Text>();
			choose_text.text = choose_name_list[i];
		}
	}
}
