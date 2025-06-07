using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IFsm
{
    void ChangeState(BaseState nextState);
}

public class BaseFsm
{
    private BaseState currentState;
    private BaseState nextState;
    public void InitFsm(BaseState firstState)
    {
        currentState = firstState;
    }
    public void ChangeState(BaseState nextState)
    {
        this.nextState = nextState;
        if (currentState == nextState) return;

        currentState.Exit();
        currentState = this.nextState;
        currentState.Enter();

    }
    public void UpdateStata()
    {
        currentState?.Update();
    }
}
