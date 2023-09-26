using UnityEngine;

public class ReceivingState : IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        switch (baseAIBots.BotType)
        {
            case BotType.ForwardLeft:
                //BallManager.Instance.AttachBall(baseAIBots.transform);
                //stateController.ChangeState(stateController.attackState, 0f);
                break;
            
            case BotType.ForwardRight:
                //BallManager.Instance.AttachBall(baseAIBots.transform);
                break;
            
            case BotType.DefenderRightBack:
                //MoveToTheBall(stateController, baseAIBots);
                break;
            
            case BotType.DefenderLeftBack:
                break;
        } 
        
        
        if (BallManager.Instance.BallAttached)
        {
            stateController.ChangeState(stateController.attackState, 0f);
        }
    }
    
    
    public void OnUpdate(StateController stateController, BaseAIBots baseAIBots)
    {
        switch (baseAIBots.BotType)
        {
            case BotType.ForwardLeft:
                if (BallManager.Instance.transform.parent == null)
                {
                    MoveToTheBall(stateController, baseAIBots);
                }
                else if (BallManager.Instance.transform.parent is not null)
                {
                    stateController.ChangeState(stateController.patrollingState, 0f);
                }
                break;
            
            case BotType.ForwardRight:
                if (BallManager.Instance.transform.parent == null)
                {
                    MoveToTheBall(stateController, baseAIBots);
                }
                else if (BallManager.Instance.transform.parent is not null)
                {
                    stateController.ChangeState(stateController.patrollingState, 0f);
                }
                break;
            
            case BotType.DefenderLeftBack:
                if (BallManager.Instance.transform.parent == null)
                {
                    MoveToTheBall(stateController, baseAIBots);
                }
                else if (BallManager.Instance.transform.parent is not null)
                {
                    stateController.ChangeState(stateController.patrollingState, 0f);
                }
                break;
            
            case BotType.DefenderRightBack:
                if (BallManager.Instance.transform.parent == null)
                {
                    MoveToTheBall(stateController, baseAIBots);
                }
                else if (BallManager.Instance.transform.parent is not null)
                {
                    stateController.ChangeState(stateController.patrollingState, 0f);
                }
                break;
        } 
    }

    private void MoveToTheBall(StateController stateController, BaseAIBots baseAIBots)
    {
        //find the ball if it's free then attack to the ball
        
        Transform ball = null;
        ball = BallManager.Instance.transform;
        
        float distance = Vector3.Distance(baseAIBots.transform.position, BallManager.Instance.transform.position);

        baseAIBots.transform.LookAt(ball.transform);
        
        Vector3 movedDirection = 
            ball.position - new Vector3(baseAIBots.transform.position.x, 0, baseAIBots.transform.position.z);
    
        Vector3 moveSpeed = new Vector3(movedDirection.normalized.x * baseAIBots.RunSpeed * Time.deltaTime, 0,
            movedDirection.normalized.z * baseAIBots.RunSpeed * Time.deltaTime);

        baseAIBots.transform.position += moveSpeed;
        
        if (distance < 1f)
        {
            switch (baseAIBots.BotType)
            {
                case BotType.ForwardLeft:
                    BallManager.Instance.AttachBall(baseAIBots.transform);
                    stateController.ChangeState(stateController.attackState, 0f);
                    break;
                case BotType.ForwardRight:
                    BallManager.Instance.AttachBall(baseAIBots.transform);
                    stateController.ChangeState(stateController.attackState, 0f);
                    break;
                
                case BotType.DefenderLeftBack:
                    BallManager.Instance.AttachBall(baseAIBots.transform);
                    stateController.ChangeState(stateController.patrollingState, 0f);
                    break;
                
                case BotType.DefenderRightBack:
                    BallManager.Instance.AttachBall(baseAIBots.transform);
                    stateController.ChangeState(stateController.patrollingState, 0f);
                    break;

            }
        }
    }

    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}