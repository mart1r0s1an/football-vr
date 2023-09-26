using UnityEngine;

public class StandingState : IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        switch (baseAIBots.BotType)
        {
            case BotType.ForwardLeft:
                baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsPassingBallHash, false);
                baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, true);
                stateController.ChangeState(stateController.patrollingState, 0f);
                break;
            
            case BotType.ForwardRight:
                baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsPassingBallHash, false);
                baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, true);
                stateController.ChangeState(stateController.patrollingState, 0f);
                break;
            
            case BotType.DefenderRightBack:
                baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsPassingBallHash, false);
                baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, true);
                stateController.ChangeState(stateController.patrollingState, 0f);
                break;
            
            case BotType.DefenderLeftBack:
                baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsPassingBallHash, false);
                baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, true);
                stateController.ChangeState(stateController.patrollingState, 0f);
                break;
        } 
    }
    
    public void OnUpdate(StateController stateController, BaseAIBots baseAIBots)
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            stateController.ChangeState(stateController.runningState, 0f);
        }
    }
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        switch (baseAIBots.BotType)
        {
            case BotType.ForwardLeft:
                break;
            
            case BotType.ForwardRight:
                break;
            
            case BotType.DefenderRightBack:
                break;
            
            case BotType.DefenderLeftBack:
                break;
        } 
    }
}
