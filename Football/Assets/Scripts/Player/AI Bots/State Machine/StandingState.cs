using UnityEngine;

public class StandingState : IPlayerState
{
    public void OnEnter(StateController stateController)
    {
        
    }

    public void OnUpdate(StateController stateController)
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            stateController.ChangeState(stateController.runningState);
        }
    }
    
    public void OnExit(StateController stateController)
    {
        
    }
}
