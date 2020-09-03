using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CalenderWindow : WindowBase {
	public List<Transform> m_days_list = new List<Transform>();
	public Transform go_btn_tran;
	private int m_pin_scene = 0;
	void Awake()
	{
		Messenger.AddListener<Transform>("calender_pin_drag", PinDraggable);
		Messenger.AddListener<Transform>("calender_pin_down", PinDown);
		Messenger.AddListener("calender_show", ShowCalender);
	}
	void PinDown(Transform tran)
	{
		float pin_distance = 30;
		int cur_day = Singleton<SceneManager>.Instance.m_cur_day;
		float dist = Vector3.Distance(tran.position, m_days_list[cur_day].position);

		DraggablePin pin = tran.GetComponent<DraggablePin>();
		if (dist < pin_distance)
		{
			CalenderModel.SetDayPin(cur_day, pin.idx);
			tran.position = m_days_list[cur_day].position;
			tran.localEulerAngles = new Vector3(0, 0, 0);
			m_pin_scene = pin.idx;
			go_btn_tran.gameObject.SetActive(true);
		}
		else
		{
			pin.ReturnOrigion();
			pin.ReturnOrigionRotation();
			go_btn_tran.gameObject.SetActive(false);
		}
	} 

	void PinDraggable(Transform tran)
	{		
		float pin_distance = 30;
		int cur_day = Singleton<SceneManager>.Instance.m_cur_day;
		float dist = Vector3.Distance(tran.position, m_days_list[cur_day].position);
			
		DraggablePin pin = tran.GetComponent<DraggablePin>();
		if (dist < pin_distance)
		{
			CalenderModel.SetDayPin(cur_day, pin.idx);
			tran.position = m_days_list[cur_day].position;
			tran.localEulerAngles = new Vector3(0, 0, 0);
		}
		else
		{
			pin.ReturnOrigionRotation();
		}
	}
	
	public void ShowCalender()
	{
		Active(true);
		go_btn_tran.gameObject.SetActive(false);
	}

	public void CloseCalender()
	{
		int scene = GetSelectedScene();
		Messenger.Broadcast<int>("calender_close", scene);
		Active(false);
	}
	public int GetSelectedScene()
	{
		return m_pin_scene;
	}
}
