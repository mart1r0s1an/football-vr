using UnityEngine;

public class ReceivingState : IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        if (BallManager.Instance.BallAttached is true)
        {
            stateController.ChangeState(stateController.attackState);
        }
    }

    public void OnUpdate(StateController stateController, CharacterController characterController, BaseAIBots baseAIBots)
    {
        
    }
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}
