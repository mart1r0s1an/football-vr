public class StandingState : IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsPassingBallHash, false);
        baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, true);
        stateController.ChangeState(stateController.patrollingState, 0f);
    }
    
    public void OnUpdate(StateController stateController, BaseAIBots baseAIBots)
    {
    }
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
    }
}
