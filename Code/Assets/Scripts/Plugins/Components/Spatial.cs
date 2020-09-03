using UnityEngine;
using System.Collections;

public class Spatial : MonoBehaviour {
    private Vector3 m_position;
    private Vector3 m_direction;
    private float m_speed;
	[SerializeField]
	private float m_max_speed;

    public Vector3 Position
    {
        get
        {
            return m_position;
        }
        set
        {
            m_position = value;
        }
    }
    public Vector3 Direction
    {
        get
        {
            return m_direction;
        }
        set
        {
            m_direction = value;
        }
    }
    public float Speed
    {
        get
        {
            return m_speed;
        }
        set
        {
            m_speed = value;
        }
    }
	public float MaxSpeed
	{
		get
		{
			return m_max_speed;
		}
		set
		{
			m_max_speed = value;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
        	
	// Update is called once per frame
	void Update () {
        //todo set position, rotation
	}

	public void Move(Vector3 position)
	{
		Rigidbody2D rigid_body = GetComponent<Rigidbody2D>();
		if(rigid_body != null)
		{
			rigid_body.MovePosition(position);
		}
		else
		{
			Position = position;
		}
	}

	public void Stop()
	{
		Rigidbody2D rigid_body = GetComponent<Rigidbody2D>();
		if(rigid_body != null)
		{
			rigid_body.velocity = Vector2.zero;
		}
	}

	/* static method */
	public static int GetDirectionValue(Vector3 direction)
	{
		int result = 0;
		if(direction.x == 1.0f && direction.y == 0)
		{
			result = 1;
		}
		else if(direction.x == 0 && direction.y == 1.0f)
		{
			result = 2;
		} 
		else if(direction.x == -1.0f && direction.y == 0)
		{
			result = 3;
		} 
		else if(direction.x == 0 && direction.y == -1)
		{
			result = 4;
		} 
		return result;
	}
}
