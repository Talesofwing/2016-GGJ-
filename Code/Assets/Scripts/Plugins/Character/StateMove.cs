using UnityEngine;
using System.Collections;

public class StateMove : FSMState {

    public StateMove()
    {
        stateID = FSMStateId.STATE_MOVE;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public override void Act(GameObject player)
    {
        Spatial spatial = player.GetComponent<Spatial>();
//        spatial.Position += spatial.Direction * spatial.Speed * Time.deltaTime;
		Animator animator = player.GetComponentInChildren<Animator>();
		if(animator != null)
		{
			int direction = Spatial.GetDirectionValue(spatial.Direction);
			float speed = spatial.Speed;
			animator.SetInteger("Direction", direction);
			animator.SetFloat("Speed", speed);
		}
    }
	public override void FixedAct(GameObject player)
	{
        Spatial spatial = player.GetComponent<Spatial>();
//        spatial.Position += spatial.Direction * spatial.Speed * Time.deltaTime;
		Vector3 dest = player.transform.localPosition + spatial.Direction * spatial.Speed * Time.fixedDeltaTime;// Time.deltaTime;
//		Debug.Log("fixed act:" + player.transform.localPosition.ToString() + " speed= " + spatial.Speed + " dest="+dest.ToString());
		spatial.Move(dest);
	}
    public override void Reason(GameObject player)
    {
        Spatial spatial = player.GetComponent<Spatial>();
        if (spatial != null && spatial.Speed == 0)
        {
            Character character = player.GetComponent<Character>();
            if (character != null)
            {
                character.SetTransition(FSMTransition.STOP_MOVE);
            }
            
        }
    }
}
