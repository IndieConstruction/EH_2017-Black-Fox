using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase {

    public virtual void OnStart() { }

    public virtual void OnUpdate() { }

    public virtual void OnEnd() { }

    #region Events
    public delegate void StateEvent(string _stateName);

    public static StateEvent OnStateEnd;
    #endregion

}
