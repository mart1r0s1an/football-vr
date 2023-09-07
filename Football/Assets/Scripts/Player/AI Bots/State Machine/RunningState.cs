using UnityEngine;

public class RunningState : IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        switch (baseAIBots.BotType)
        {
            case BotType.ForwardLeft:
                //stateController.transform.position = baseAIBots.ForwardLeftStartPosition.position;
                BotAnimationState.BotAnimatorController.SetBool("isRunningWithBall", true);
                
                break;
            
            case BotType.ForwardRight:
                //stateController.transform.position = baseAIBots.ForwardRightStartPosition.position;
                BotAnimationState.BotAnimatorController.SetBool("isRunningWithBall", true);
                break;
            
            case BotType.DefenderLeftBack:
                //stateController.transform.position = baseAIBots.DefenderLeftBackStartPosition.position;
                BotAnimationState.BotAnimatorController.SetBool("isRunningWithBall", true);
                
                break;
            
            case BotType.DefenderRightBack:
                //stateController.transform.position = baseAIBots.DefenderRightBackStartPosition.position;
                BotAnimationState.BotAnimatorController.SetBool("isRunningWithBall", true);
                break;
        }
    }

    public void OnUpdate(StateController stateController, CharacterController characterController,
        BaseAIBots baseAIBots)
    {
        Vector3 movedirection = baseAIBots.Ball.position
                                - new Vector3(baseAIBots.transform.position.x, 0, baseAIBots.transform.position.z);
        
        float distanceToBall = movedirection.magnitude;
        
        Vector3 moveSpeed = new Vector3(movedirection.normalized.x * baseAIBots.RunSpeed * Time.deltaTime, 0,
            movedirection.normalized.z * 10 * Time.deltaTime);
        
        baseAIBots.transform.position += moveSpeed;
        baseAIBots.transform.LookAt(baseAIBots.Ball);
    }

    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}
