using UnityEngine;
using System.Collections;

public class WindowBase : MonoBehaviour {	
	public Transform m_content_root;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Active(bool visible)
	{
		if(m_content_root!= null)
		{
			m_content_root.gameObject.SetActive(visible);
		}
	}
}
