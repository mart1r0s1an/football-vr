using UnityEngine;

public class KickingState : IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsPassingBallHash, true);
        
        BallManager.Instance.KickTheBall(baseAIBots.KickBallForce);
        
        stateController.ChangeState(stateController.standingState);
    }

    public void OnUpdate(StateController stateController, CharacterController characterController, BaseAIBots baseAIBots)
    {
        
    }
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}
