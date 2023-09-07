using UnityEngine;

public class StandingState : IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }

    public void OnUpdate(StateController stateController, CharacterController characterController, BaseAIBots baseAIBots)
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            stateController.ChangeState(stateController.runningState);
        }
    }
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}
