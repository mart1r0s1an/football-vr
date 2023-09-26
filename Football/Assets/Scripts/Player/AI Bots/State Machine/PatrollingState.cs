using UnityEngine;

public class PatrollingState : IPlayerState
{
    private bool _walkPointSet;
    private Vector3 _walkPoint;
    
    private float _shortestDistance;
    private float _canPassTheBallDistance;
    
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        _shortestDistance = stateController.AttackToTheAttackerWithTheBall;
    }

    public void OnUpdate(StateController stateController, BaseAIBots baseAIBots)
    {
        Patrolling(baseAIBots);
        
        if (stateController.ClosestLocalPlayer != null)
        {
            GetClosestPlayer(stateController, baseAIBots);
        }
        
        if (BallManager.Instance.transform.parent == null && !stateController.AlreadyPassed)
        {
            GetFreeBall(stateController, baseAIBots);
        }
    }

    private void GetFreeBall(StateController stateController, BaseAIBots baseAIBots)
    {
        float distance = Vector3.Distance(baseAIBots.transform.position, BallManager.Instance.transform.position);
            
        if (distance < _shortestDistance)
        {
            if (!baseAIBots.HasBall)
            {
                stateController.ChangeState(stateController.receivingState, 0f);
            }
        }
    }

    private void GetClosestPlayer(StateController stateController, BaseAIBots baseAIBots)
    {
        if (stateController.ClosestLocalPlayer.CompareTag("BotOpponent") && stateController.ClosestLocalPlayer.GetComponent<PlayerBase>().HasBall)
        {
            switch (baseAIBots.BotType)
            {
                case BotType.ForwardLeft:
                    break;
            
                case BotType.ForwardRight:
                
                    break;
            
                case BotType.DefenderLeftBack:
                    baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, true);
                
                    Vector3 moveDirectionForLeftBack = stateController.ClosestLocalPlayer.transform.position
                                                       - new Vector3(baseAIBots.transform.position.x, 0, baseAIBots.transform.position.z);
        
                    float distanceToPlayerForLeftBack = moveDirectionForLeftBack.magnitude;
        
                    Vector3 moveSpeedForLeftBack = new Vector3(moveDirectionForLeftBack.normalized.x * baseAIBots.RunSpeed * Time.deltaTime, 0,
                        moveDirectionForLeftBack.normalized.z * Time.deltaTime);
        
                    baseAIBots.transform.position += moveSpeedForLeftBack;
                
                    baseAIBots.transform.LookAt(stateController.ClosestLocalPlayer.transform);
                    
                    
                    if (distanceToPlayerForLeftBack <= 2f && stateController.ClosestLocalPlayer.GetComponent<PlayerBase>().HasBall)
                    {
                        BallManager.Instance.DetachBall(stateController.ClosestLocalPlayer);
                        
                        stateController.ChangeState(stateController.receivingState, 0f);
                        
                        baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithOutBallHash, true);
                    }
                    break;
            
                case BotType.DefenderRightBack:
                    baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, true);
                
                    Vector3 moveDirection = stateController.ClosestLocalPlayer.transform.position
                                            - new Vector3(baseAIBots.transform.position.x, 0, baseAIBots.transform.position.z);
        
                    float distanceToPlayer = moveDirection.magnitude;
        
                    Vector3 moveSpeed = new Vector3(moveDirection.normalized.x * baseAIBots.RunSpeed * Time.deltaTime, 0,
                        moveDirection.normalized.z * Time.deltaTime);
        
                    baseAIBots.transform.position += moveSpeed;
                
                    baseAIBots.transform.LookAt(stateController.ClosestLocalPlayer.transform);
                    
                    
                    if (distanceToPlayer <= 2f && stateController.ClosestLocalPlayer.GetComponent<PlayerBase>().HasBall)
                    {
                        BallManager.Instance.DetachBall(stateController.ClosestLocalPlayer);
                        
                        stateController.ChangeState(stateController.receivingState, 0f);
                        
                        baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithOutBallHash, true);
                    }
                    break;
            }
        }
        
        if (stateController.ClosestLocalPlayer.CompareTag("Bot") && !stateController.ClosestLocalPlayer.GetComponent<PlayerBase>().HasBall)
        {
            switch (baseAIBots.BotType)
            {
                case BotType.ForwardLeft:
                    
                    break;
            
                case BotType.ForwardRight:
                    
                    break;
            
                case BotType.DefenderLeftBack:
                    if (baseAIBots.HasBall)
                    {
                        baseAIBots.transform.LookAt(stateController.ClosestLocalPlayer);

                        BallManager.Instance.transform.parent = stateController.ClosestLocalPlayer;
                        
                        stateController.ChangeState(stateController.passingState, 0f);
                    }
                    break;
            
                case BotType.DefenderRightBack:

                    if (baseAIBots.HasBall)
                    {
                        baseAIBots.transform.LookAt(stateController.ClosestLocalPlayer);
                        
                        stateController.ChangeState(stateController.passingState, 0f);
                    }
                    break;
            }
        }
    }
    
    private void Patrolling(BaseAIBots botTransform)
    {
        if (!_walkPointSet) 
            SearchWalkPoint(botTransform);
        
        Vector3 movedDirection = 
            _walkPoint - new Vector3(botTransform.transform.position.x, 0, botTransform.transform.position.z);
    
        Vector3 moveSpeed = new Vector3(movedDirection.normalized.x * botTransform.PatrollingStateRunSpeed * Time.deltaTime, 0,
            movedDirection.normalized.z * botTransform.PatrollingStateRunSpeed * Time.deltaTime);

        if (_walkPointSet)
        {
            botTransform.transform.position += moveSpeed;
            botTransform.transform.LookAt(_walkPoint);
        }
    
        Vector3 distanceToWalkPoint = botTransform.transform.position - _walkPoint;
    
        if (distanceToWalkPoint.magnitude < 1f)
            _walkPointSet = false;
    }

    private void SearchWalkPoint(BaseAIBots botTransform)
    {
        float randomZ = Random.Range(-15, 15);
        float randomX = Random.Range(-15, 15);
        
        _walkPoint = new Vector3(botTransform.transform.position.x + randomX, 
            botTransform.transform.position.y, botTransform.transform.position.z + randomZ);
        
        if (Physics.Raycast(_walkPoint, -botTransform.transform.up, 2f, botTransform.groundLayer))
        {
            _walkPointSet = true;   
        }
    }
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}