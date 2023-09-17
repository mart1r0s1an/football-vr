using UnityEngine;

public class RunningState : IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        switch (baseAIBots.BotType)
        {
            case BotType.ForwardLeft:
                baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithOutBallHash, true);
                break;
            
            case BotType.ForwardRight:
                baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithOutBallHash, true);
                break;
            
            case BotType.DefenderLeftBack:
                baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithOutBallHash, true);
                
                break;
            
            case BotType.DefenderRightBack:
                baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithOutBallHash, true);
                break;
        }
    }

    public void OnUpdate(StateController stateController, CharacterController characterController,
        BaseAIBots baseAIBots)
    {
        
        switch (baseAIBots.BotType)
        {
            case BotType.ForwardLeft:
                baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, true);
                Vector3 movedirection = baseAIBots.Ball.position
                                        - new Vector3(baseAIBots.transform.position.x, 0, baseAIBots.transform.position.z);
        
                float distanceToBall = movedirection.magnitude;
        
                Vector3 moveSpeed = new Vector3(movedirection.normalized.x * baseAIBots.RunSpeed * Time.deltaTime, 0,
                    movedirection.normalized.z * 10 * Time.deltaTime);
        
                baseAIBots.transform.position += moveSpeed;
                baseAIBots.transform.LookAt(baseAIBots.Ball);
                
                if (distanceToBall < 1.3f)
                {
                    baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, false);
                    stateController.ChangeState(stateController.receivingState, 0f);
                }
                break;
            
            case BotType.ForwardRight:
                Debug.Log("forward right");
                break;
            
            case BotType.DefenderLeftBack:
               
                
                break;
            
            case BotType.DefenderRightBack:
                baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, true);
                Vector3 movedirectionForRightDefender = baseAIBots.Ball.position
                                        - new Vector3(baseAIBots.transform.position.x, 0, baseAIBots.transform.position.z);
        
                float distanceToBallForRightDefender = movedirectionForRightDefender.magnitude;
        
                Vector3 moveSpeedRightDefender = new Vector3(movedirectionForRightDefender.normalized.x * baseAIBots.RunSpeed * Time.deltaTime, 0,
                    movedirectionForRightDefender.normalized.z * 10 * Time.deltaTime);
        
                baseAIBots.transform.position += moveSpeedRightDefender;
                baseAIBots.transform.LookAt(baseAIBots.Ball);
                
                if (distanceToBallForRightDefender < 1.3f)
                {
                    baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, false);
                    stateController.ChangeState(stateController.receivingState, 0f);
                }
                break;
        }
    }

    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}
