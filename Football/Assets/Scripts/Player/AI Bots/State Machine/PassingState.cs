using UnityEngine;

public class PassingState : IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsPassingBallHash, true);
        
        if (baseAIBots.HasBall)
        {
            BallManager.Instance.PassTheBall(baseAIBots.KickBallForce);
            BallManager.Instance.DetachBall(baseAIBots.transform);
            
            stateController.ChangeState(stateController.standingState, 0f);
        }
    }

    public void OnUpdate(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}
