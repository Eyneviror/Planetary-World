using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IState
{
    public void Enter();
    public void LateUpdate();
    public void Update();
    public void Exit();
}

public class Context
{
    private IState currentState;

    public void TransitionTo(IState state)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = state;
        currentState.Enter();
    }

    public void UpdateState()
    {
        if (currentState != null)
            currentState.Update();
    }
    public void LateUpdateState()
    {
        if (currentState != null)
            currentState.Update();
    }
}
