using UnityEngine;

public class StandingState : IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        switch (baseAIBots.BotType)
        {
            case BotType.ForwardLeft:
                stateController.transform.position = baseAIBots.ForwardLeftStartPosition.position;
                break;
        }
    }

    public void OnUpdate(StateController stateController, CharacterController characterController)
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
