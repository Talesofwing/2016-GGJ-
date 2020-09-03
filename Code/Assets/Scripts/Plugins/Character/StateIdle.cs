using UnityEngine;
using System.Collections;

public class StateIdle : FSMState {    
    public StateIdle()
    {
        stateID = FSMStateId.STATE_IDLE;
    }

	// Use this for initialization
	void Start () {	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public override void Act(GameObject player)
    {
        //throw new System.NotImplementedException();
        Spatial spatial = player.GetComponent<Spatial>();
		Animator animator = player.GetComponentInChildren<Animator>();
		if(animator != null)
		{
			float speed = spatial.Speed;
			animator.SetFloat("Speed", speed);
		}
    }
	public override void FixedAct(GameObject player)
	{
		Spatial spatial = player.GetComponent<Spatial>();
		spatial.Stop();
	}

    public override void Reason(GameObject player)
    {
        Spatial spatial = player.GetComponent<Spatial>();
        if (spatial != null && spatial.Speed > 0)
        {
            Character character = player.GetComponent<Character>();
            if (character != null)
            {
                character.SetTransition(FSMTransition.MOVE);
            }
        }

    }
}
