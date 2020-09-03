using UnityEngine;
using System.Collections;

public class Singleton<T> where T : MonoBehaviour, new()
{
	private static T m_instance;
	private static volatile Object padlock = new Object();
	public static T Instance
    {
        get
        {
			if (m_instance == null)
			{
				lock (padlock)
				{
					if (m_instance == null)
					{
						m_instance = GameManager.m_trans.GetComponent<T>();
						if (m_instance == null)
						{
							m_instance = GameManager.m_trans.gameObject.AddComponent<T>();
							Debug.Log("instance:" + typeof(T).ToString());
						}
					}
				}
			}
			return m_instance;
			//return Nested.instance;
		}
		set
		{
			m_instance = value;
		}
    }
	

	class Nested
	{
		static Nested()
		{
		}

		internal static readonly T instance = new T();
	}
}
