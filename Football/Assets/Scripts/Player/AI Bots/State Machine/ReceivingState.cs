using UnityEngine;

public class ReceivingState : IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        switch (baseAIBots.BotType)
        {
            case BotType.ForwardLeft:
               BallManager.Instance.AttachBall(baseAIBots.transform);
                break;
            
            case BotType.ForwardRight:
                BallManager.Instance.AttachBall(baseAIBots.transform);
                break;
            
            case BotType.DefenderRightBack:
                BallManager.Instance.AttachBall(baseAIBots.transform);
                break;
            
            case BotType.DefenderLeftBack:
                BallManager.Instance.AttachBall(baseAIBots.transform);
                break;
        } 
        
        
        if (BallManager.Instance.BallAttached is true)
        {
            stateController.ChangeState(stateController.attackState, 0f);
        }
    }

    public void OnUpdate(StateController stateController, CharacterController characterController, BaseAIBots baseAIBots)
    {
    }
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}
