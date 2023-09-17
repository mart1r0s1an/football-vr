using UnityEngine;

public class AttackState : IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        //attack to the goal
    }

    public void OnUpdate(StateController stateController, CharacterController characterController, BaseAIBots baseAIBots)
    {
        switch (baseAIBots.BotType)
        {
            case BotType.ForwardLeft:
                baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, true);
                Vector3 movedirection = baseAIBots.FirstHalfGoalPosition.position
                                        - new Vector3(baseAIBots.transform.position.x, 0, baseAIBots.transform.position.z);
        
                float distanceToGoal = movedirection.magnitude;
        
                Vector3 moveSpeed = new Vector3(movedirection.normalized.x * baseAIBots.RunSpeed * Time.deltaTime, 0,
                    movedirection.normalized.z * baseAIBots.RunSpeed * Time.deltaTime);
        
                baseAIBots.transform.position += moveSpeed;
                baseAIBots.transform.LookAt(baseAIBots.FirstHalfGoalPosition);
                
                if (distanceToGoal <= 20f)
                {
                    baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, false);
                    baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithOutBallHash, false);
                    stateController.ChangeState(stateController.kickingState, 0f);
                }
                break;
            
            case BotType.ForwardRight:
                
                break;
            
            case BotType.DefenderLeftBack:
               
                
                break;
            
            case BotType.DefenderRightBack:
                Debug.Log("Do something cool here");
                
                
                break;
        }
    }
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}
