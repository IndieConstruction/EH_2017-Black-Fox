using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase {

    protected StateMachineBase stateMachine;

    public void OnPreStart(StateMachineBase _stateMachine)
    {
        stateMachine = _stateMachine;
        OnStart();
    }

    public virtual void OnStart() { }

    public virtual void OnUpdate() { }

    public virtual void OnEnd() { }

    #region Events
    public delegate void StateEvent(string _string);

    public static StateEvent OnStateEnd;
    #endregion

}
