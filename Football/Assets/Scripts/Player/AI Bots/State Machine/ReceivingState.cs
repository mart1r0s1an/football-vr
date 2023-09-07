using UnityEngine;

public class ReceivingState : IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        Debug.Log("hello from receiving state");
    }

    public void OnUpdate(StateController stateController, CharacterController characterController, BaseAIBots baseAIBots)
    {
        
    }
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}
