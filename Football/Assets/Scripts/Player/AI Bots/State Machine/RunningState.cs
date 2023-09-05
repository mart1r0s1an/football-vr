using UnityEngine;

public class RunningState : IPlayerState
{
    public void OnEnter(StateController stateController)
    {
        Debug.Log("Hello ");
    }

    public void OnUpdate(StateController stateController)
    {
       
    }
    
    public void OnExit(StateController stateController)
    {
        
    }
}
