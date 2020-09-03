using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    FSM m_fsm;
    int m_sid;
    Spatial m_spatial;
    Fightable m_fighter;
    

    public int SID
    {
        get
        {
            return m_sid;
        }
        set
        {
            m_sid = value;
        }
    }
    
    void Awake()
    {
        m_fsm = new FSM();
    }

	void Start () {        
        if (m_fsm != null)
        {
            StateMove stateMove = new StateMove();
            StateIdle stateIdle = new StateIdle();            
            stateIdle.AddTransition(FSMTransition.MOVE, FSMStateId.STATE_MOVE);
            stateMove.AddTransition(FSMTransition.STOP_MOVE, FSMStateId.STATE_IDLE);
            m_fsm.AddState(stateIdle);
            m_fsm.AddState(stateMove);
            m_fsm.CurrentState = stateIdle;            
        }
        
        m_spatial = gameObject.GetComponent<Spatial>();
        m_fighter = gameObject.GetComponent<Fightable>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        m_fsm.CurrentState.Reason(gameObject);
        m_fsm.CurrentState.Act(gameObject);
	}

	void FixedUpdate()
	{
		m_fsm.CurrentState.FixedAct(gameObject);
	}


    //meber operation
    public void SetTransition(FSMTransition transition)
    {
        m_fsm.PerformTransition(transition);
    }
}
