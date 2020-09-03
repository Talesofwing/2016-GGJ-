using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {
	private int m_distance = 1000;
	// Use this for initialization
	void Start () {
	
	}

	public void Act() 
	{
		Spatial spatial = transform.GetComponent<Spatial>();

//		int mask = LayerMask.NameToLayer("Obstacle");//LayerMask.GetMask("Obstacle");
		int mask = 1 << 10;
		RaycastHit2D hit_result = Physics2D.Raycast(transform.position, spatial.Direction, m_distance, mask);
		if(hit_result.transform != null)
		{
//			Debug.Log(hit_result.transform.gameObject.name);
			ReActor reactor = hit_result.transform.GetComponent<ReActor>();
			if(reactor != null)
			{
				reactor.ReAct();
			}
		}
	}
}
