using UnityEngine;
using System.Collections;

public class InputDetector : MonoBehaviour {
    public const string MOVE = "MOVE";
    //private Vector3 m_cur_direction = Vector3.zero;
    private GameObject m_cur_obj = null;    
    private Spatial m_cur_spatial = null;
	private Actor m_actor = null;
    public GameObject CurObj
    {
        get
        {
            if (m_cur_obj == null)
            {
                m_cur_obj = transform.gameObject;
            }
            return m_cur_obj;
        }
        set
        {
            m_cur_obj = value;
        }
    }

    public Spatial CurSpatial
    {
        get
        {
            if (m_cur_spatial == null)
            {
                m_cur_spatial = transform.GetComponent<Spatial>();
            }
            return m_cur_spatial;
        }
        set
        {
            m_cur_spatial = value;
        }
    }
	public Actor CurActor
	{
		get
		{
			if(m_actor == null)
			{
				m_actor = transform.GetComponent<Actor>();
			}
			return m_actor;
		}
		set
		{
			m_actor = value;
		}
	}
	// Update is called once per frame
	void Update () {
        Vector3 direction = Vector3.zero;
        float speed = 0.0f;
        if(Input.anyKey)
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                direction = Vector3.up;
            }
			else
            if(Input.GetKey(KeyCode.DownArrow))
            {
                direction = Vector3.down;
            }
			else
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction = Vector3.left;
            }
			else
            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction = Vector3.right;
            }
        }
        if(direction.magnitude > 0)
		{
			direction = Vector3.Normalize(direction);
			CurSpatial.Direction = direction;

			speed = CurSpatial.MaxSpeed;
		}
		CurSpatial.Speed = speed;

		if(Input.GetKeyDown(KeyCode.Z))
		{
			//CurActor.Act();
			Messenger.Broadcast("input_z");
		}

		if(Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			Messenger.Broadcast("input_enter");
		}
	}
}
