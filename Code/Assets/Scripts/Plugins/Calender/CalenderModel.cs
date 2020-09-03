using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CalenderModel : MonoBehaviour {
	
	public static Dictionary<int, int> m_day_pin_map = new Dictionary<int, int>();
	
	public static void SetDayPin(int day, int pin)
	{
		if(!m_day_pin_map.ContainsKey(day))
		{
			m_day_pin_map.Add(day, pin);
		}
	}
}
