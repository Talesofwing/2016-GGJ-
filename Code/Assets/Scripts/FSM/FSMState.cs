using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum FSMTransition
{
    NullTransition = 0,
    MOVE,
    STOP_MOVE
}

public enum FSMStateId
{
    NullState = 0,
    STATE_IDLE = 1,
    STATE_MOVE = 2
}

public abstract class FSMState
{
	public uint m_transitionNum;
    //public int[] m_inputArray;
    //public int[] m_outputArray;
    protected Dictionary<FSMTransition, FSMStateId> map = new Dictionary<FSMTransition, FSMStateId>();
    protected FSMStateId stateID;
    public FSMStateId StateID
    {
        get
        {
            return stateID;
        }
    }
	public FSMState()
	{

	}
	~FSMState()
	{
	}

    public void AddTransition(FSMTransition trans, FSMStateId id)
    {
        if (trans == FSMTransition.NullTransition)
        {
            return;
        }
        if (id == FSMStateId.NullState)
        {
            return;
        }
        if (map.ContainsKey(trans))
        {
            return;
        }
        map.Add(trans, id);
    }

    public void DeleteTransition(FSMTransition trans)
    {
        if (trans == FSMTransition.NullTransition)
        {
            return;
        }

        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }
    }

    public FSMStateId GetOutputStateId(FSMTransition trans)
    {
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }
        return FSMStateId.NullState;
    }

    public virtual void DoBeforeEntering()
    {

    }

    public virtual void DoBeforeLeaving()
    {

    }

    public abstract void Reason(GameObject player);

    public abstract void Act(GameObject player);
	public virtual void FixedAct(GameObject player)
	{

	}
}
