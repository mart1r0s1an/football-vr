using UnityEngine;

public class AttackState : IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        if (stateController.CompareTag("Bot"))
        {
            Debug.Log(stateController.ClosestLocalPlayerToTheGoal);

            if (Random.Range(0,2) == 0)
            {
                baseAIBots.transform.LookAt(stateController.ClosestLocalPlayerToTheGoal);
                stateController.ChangeState(stateController.passingState, 0f);
            }
        }
        else if (stateController.CompareTag("BotOpponent"))
        {
            Debug.Log(stateController.ClosestLocalOpponent);

            if (Random.Range(0,2) == 0)
            {
                baseAIBots.transform.LookAt(stateController.ClosestLocalOpponent);
                stateController.ChangeState(stateController.passingState, 0f);
            }
        }
        
    }
    
    public void OnUpdate(StateController stateController, BaseAIBots baseAIBots)
    {
        if (baseAIBots.HasBall)
        {
            switch (baseAIBots.BotType)
            {
                case BotType.ForwardLeft:
                    
                    baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, true);
                    Vector3 moveDirection = baseAIBots.GoalPosition.position
                                            - new Vector3(baseAIBots.transform.position.x, 0, baseAIBots.transform.position.z);
        
                    float distanceToGoal = moveDirection.magnitude;
        
                    Vector3 moveSpeed = new Vector3(moveDirection.normalized.x * baseAIBots.RunSpeed * Time.deltaTime, 0,
                        moveDirection.normalized.z * baseAIBots.RunSpeed * Time.deltaTime);
        
                    baseAIBots.transform.position += moveSpeed;
                    baseAIBots.transform.LookAt(baseAIBots.GoalPosition);
                
                    if (distanceToGoal <= 20f)
                    {
                        baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, false);
                        baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithOutBallHash, false);
                        stateController.ChangeState(stateController.kickingState, 0f);
                    }
                    break;
            
                case BotType.ForwardRight:
                    baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, true);
                    Vector3 moveDirectionForRightBot = baseAIBots.GoalPosition.position
                                            - new Vector3(baseAIBots.transform.position.x, 0, baseAIBots.transform.position.z);
        
                    float distanceToGoalForRightForward = moveDirectionForRightBot.magnitude;
        
                    Vector3 moveSpeedRight = new Vector3(moveDirectionForRightBot.normalized.x * baseAIBots.RunSpeed * Time.deltaTime, 0,
                        moveDirectionForRightBot.normalized.z * baseAIBots.RunSpeed * Time.deltaTime);
        
                    baseAIBots.transform.position += moveSpeedRight;
                    baseAIBots.transform.LookAt(baseAIBots.GoalPosition);
                
                    if (distanceToGoalForRightForward <= 20f)
                    {
                        baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, false);
                        baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithOutBallHash, false);
                        stateController.ChangeState(stateController.kickingState, 0f);
                    }
                    break;
            
                case BotType.DefenderLeftBack:
               
                
                    break;
            
                case BotType.DefenderRightBack:
                    stateController.ChangeState(stateController.patrollingState, 0f);
                    break;            
            }
        }
        
    }
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}
