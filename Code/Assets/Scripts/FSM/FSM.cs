using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;



public class FSM{
    private List<FSMState> stateList;
    private FSMStateId currentStateId;
    public FSMStateId CurrentStateId
    {
        get
        {
            return currentStateId;
        }
    }
    private FSMState currentState;
    public FSMState CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
            currentStateId = currentState.StateID;
        }
    }

    public FSM()
    {
        stateList = new List<FSMState>();
    }

    ~FSM()
    {
        stateList.Clear();        
    }

    public void AddState(FSMState state)
    {
        if (state == null)
        {
            Debug.LogError("FSM ERROR: Null State reference");
            return;
        }

        if (stateList.Count == 0)
        {
            stateList.Add(state);
            currentState = state;
            currentStateId = state.StateID;
            return;
        }

        foreach (FSMState tempState in stateList)
        {
            if (state.StateID == tempState.StateID)
            {
                Debug.LogError("state [" + state.StateID.ToString() + "] already added.");
                return;
            }
        }
        stateList.Add(state);
    }

    public void DeleteState(FSMStateId id)
    {
        if (id == FSMStateId.NullState)
        {
            return;
        }

        foreach (FSMState state in stateList)
        {
            if (state.StateID == id)
            {
                stateList.Remove(state);
                return;
            }
        }
        Debug.LogError("State [" + id + "] is not found");
    }

    public void PerformTransition(FSMTransition trans)
    {
        if (trans == FSMTransition.NullTransition)
        {
            return;
        }

        FSMStateId id = currentState.GetOutputStateId(trans);

        if (id == FSMStateId.NullState)
        {
            return;
        }

        currentStateId = id;
        foreach(FSMState state in stateList)
        {
            if(state.StateID == id)
            {
                currentState.DoBeforeLeaving();

                currentState = state;

                currentState.DoBeforeEntering();

                break;
            }
        }
    }
}
