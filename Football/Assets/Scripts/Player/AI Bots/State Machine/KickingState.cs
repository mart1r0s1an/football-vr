using UnityEngine;

public class KickingState : IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsPassingBallHash, true);


        if (baseAIBots.HasBall)
        {
            BallManager.Instance.KickTheBall(baseAIBots.KickBallForce);
            BallManager.Instance.DetachBall(baseAIBots.transform);
        }
        
        stateController.ChangeState(stateController.standingState, 2f);
    }

    public void OnUpdate(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}
