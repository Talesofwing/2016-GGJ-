using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct DialogNode
{
	public string m_name;
	public string m_content;
	public DialogNode(string content)
	{
		m_name = null;
		m_content = content;
	}

	public DialogNode(string name, string content)
	{
		m_name = name;
		m_content = content;
	}
}

public class DialogManager : MonoBehaviour
{

	//const string DIALOGMANAGER = "DialogManager";
	//const string DIALOG_BOX = "dialog_window";

	//static private DialogManager m_instance;

	//private GameObject m_dialog_box = null;	
	public List<DialogNode> m_dialog_list = new List<DialogNode>();	
	public int m_cur_content_idx = 0;

	//public static DialogManager Instance
	//{
	//	get
	//	{
	//		if(m_instance == null)
	//		{
	//			GameObject dialog_gameobj = GameObject.Find(DIALOGMANAGER);
	//			if(dialog_gameobj!=null)
	//			{
	//				m_instance = dialog_gameobj.GetComponent<DialogManager>();
	//			}
	//		}
	//		return m_instance;
	//	}
	//}
	//public GameObject DialogBox
	//{
	//	get
	//	{
	//		if(m_dialog_box == null)
	//		{
	//			m_dialog_box = GameObject.Find(DIALOG_BOX);
	//		}
	//		return m_dialog_box;
	//	}
	//	set
	//	{
	//		m_dialog_box = value;
	//	}
	//}
	public void Init()
	{
		Messenger.AddListener("dialog_next", ShowNext);
		Messenger.Broadcast("dialog_hide");		
	}

	// Use this for initialization
	void Start () {
		//m_dialog_box = GameObject.Find(DIALOG_BOX);
		//m_dialog_box.SetActive(false);
	}
	public void ShowDialog(List<DialogNode> dialog_list)
	{
		m_dialog_list = dialog_list;
		m_cur_content_idx = 0;
		Messenger.Broadcast<DialogNode>("dialog_show", dialog_list[0]);		
	}

	public void ShowDialog(DialogNode dialog)
	{
		Messenger.Broadcast<DialogNode>("dialog_show", dialog);
	}
	public void ShowNext()
	{
		++m_cur_content_idx;
		if(m_dialog_list.Count > m_cur_content_idx)
		{
			Messenger.Broadcast<DialogNode>("dialog_show", m_dialog_list[m_cur_content_idx]);
		}
		else
		{
			Messenger.Broadcast("dialog_end");
		}
	}

	public void HideDialog()
	{
		Messenger.Broadcast("dialog_hide");
	}
}
