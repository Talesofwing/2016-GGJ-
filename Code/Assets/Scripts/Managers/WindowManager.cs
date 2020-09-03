using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WINDOW_TYPE
{
	TITLE,
	DIALOG,
	CALENDER,
	COUNT
}
public class WindowManager : MonoBehaviour {

	public List<Transform> m_window_list = new List<Transform>();
	public Transform ui_root;
	public static readonly string UI_ROOT = "UI_ROOT";

	public void Init()
	{
		GameObject ui_root_obj = GameObject.Find(UI_ROOT);
		if (ui_root_obj == null)
		{
			ui_root_obj = new GameObject();
		}
		ui_root_obj.name = UI_ROOT;
		ui_root = ui_root_obj.transform;
		for (int i = 0; i < ui_root.transform.childCount; ++i)
		{			
			Transform tran = ui_root.GetChild(i);
			m_window_list.Add(tran);
			if (i != (int)WINDOW_TYPE.TITLE)
			{
				tran.GetComponent<WindowBase>().Active(false);
			}
		}
		
		//m_window_list[].gameObject.SetActiveRecursively(true);
	}


	#region interface
	public int GetActiveWindowCount()
	{
		int count = 0;
		for (int i = 0, imax = m_window_list.Count; i < imax; ++i)
		{
			if (m_window_list[i].gameObject.activeSelf)
			{
				++count;
			}
		}
		return count;
	}
	#endregion
}
