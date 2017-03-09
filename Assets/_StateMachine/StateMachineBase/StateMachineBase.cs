using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineBase : MonoBehaviour {

    private StateBase _currenState;

    public StateBase CurrentState
    {
        get { return _currenState; }
        set {
                if (_currenState != value)
                {
                    OnStateChange(value, _currenState);
                }
                _currenState = value;
            }
    }


    void OnStateChange(StateBase _newState, StateBase _oldState)
    {
        if (_oldState != null)
            _oldState.OnEnd();
        _newState.OnPreStart(this);
    }

    private void Update()
    {
        if (CurrentState != null)
            CurrentState.OnUpdate();
    }

}
