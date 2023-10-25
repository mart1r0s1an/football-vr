using UnityEngine;

public class PatrollingState : IPlayerState
{
    private float _shortestDistance;
    private float _canPassTheBallDistance;
    
    private readonly float _patrolRadius = 10f;
    private Vector3 _initialPosition;
    private Vector3 _currentDestination;
    
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        _initialPosition = baseAIBots.transform.position;
        SetRandomDestination(baseAIBots);
        
        _shortestDistance = stateController.AttackToTheAttackerWithTheBall;
    }

    public void OnUpdate(StateController stateController, BaseAIBots baseAIBots)
    {
        Patrolling(baseAIBots);
        
        if (stateController.ClosestLocalPlayer != null && stateController.CompareTag("Bot"))
        {
            GetClosestPlayer(stateController, baseAIBots);
        }

        if (stateController.ClosestLocalOpponent != null && stateController.CompareTag("BotOpponent"))
        {
            GetClosestPlayerForOpponent(stateController, baseAIBots);
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
    
    private void GetClosestPlayerForOpponent(StateController stateController, BaseAIBots baseAIBots)
    {
        if (stateController.ClosestLocalOpponent.CompareTag("Bot") 
            && stateController.ClosestLocalOpponent.GetComponent<PlayerBase>().HasBall)
        {
            switch (baseAIBots.BotType)
            {
                case BotType.ForwardLeft:
                    break;
            
                case BotType.ForwardRight:
                
                    break;
            
                case BotType.DefenderLeftBack:
                    baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, true);
                
                    Vector3 moveDirectionForLeftBack = stateController.ClosestLocalOpponent.transform.position
                                                       - new Vector3(baseAIBots.transform.position.x, 0, baseAIBots.transform.position.z);
        
                    float distanceToPlayerForLeftBack = moveDirectionForLeftBack.magnitude;
        
                    Vector3 moveSpeedForLeftBack = new Vector3(moveDirectionForLeftBack.normalized.x * baseAIBots.RunSpeed * Time.deltaTime, 0,
                        moveDirectionForLeftBack.normalized.z * Time.deltaTime);
        
                    baseAIBots.transform.position += moveSpeedForLeftBack;
                
                    baseAIBots.transform.LookAt(stateController.ClosestLocalOpponent.transform);
                    
                    
                    if (distanceToPlayerForLeftBack <= 2f && stateController.ClosestLocalOpponent.GetComponent<PlayerBase>().HasBall)
                    {
                        BallManager.Instance.DetachBall(stateController.ClosestLocalOpponent);
                        
                        stateController.ChangeState(stateController.receivingState, 0f);
                        
                        baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithOutBallHash, true);
                    }
                    break;
            
                case BotType.DefenderRightBack:
                    baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, true);
                
                    Vector3 moveDirection = stateController.ClosestLocalOpponent.transform.position
                                            - new Vector3(baseAIBots.transform.position.x, 0, baseAIBots.transform.position.z);
        
                    float distanceToPlayer = moveDirection.magnitude;
        
                    Vector3 moveSpeed = new Vector3(moveDirection.normalized.x * baseAIBots.RunSpeed * Time.deltaTime, 0,
                        moveDirection.normalized.z * Time.deltaTime);
        
                    baseAIBots.transform.position += moveSpeed;
                
                    baseAIBots.transform.LookAt(stateController.ClosestLocalOpponent.transform);
                    
                    
                    if (distanceToPlayer <= 2f && stateController.ClosestLocalOpponent.GetComponent<PlayerBase>().HasBall)
                    {
                        BallManager.Instance.DetachBall(stateController.ClosestLocalOpponent);
                        
                        stateController.ChangeState(stateController.receivingState, 0f);
                        
                        baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithOutBallHash, true);
                    }
                    break;
            }
        }
        
        if (stateController.ClosestLocalOpponent.CompareTag("BotOpponent") && !stateController.ClosestLocalOpponent.GetComponent<PlayerBase>().HasBall)
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
                        baseAIBots.transform.LookAt(stateController.ClosestLocalOpponent);

                        BallManager.Instance.transform.parent = stateController.ClosestLocalOpponent;
                        
                        stateController.ChangeState(stateController.passingState, 0f);
                    }
                    break;
            
                case BotType.DefenderRightBack:

                    if (baseAIBots.HasBall)
                    {
                        baseAIBots.transform.LookAt(stateController.ClosestLocalOpponent);
                        
                        BallManager.Instance.transform.parent = stateController.ClosestLocalOpponent;
                        
                        stateController.ChangeState(stateController.passingState, 0f);
                    }
                    break;
            }
        }
    }
    
    private void Patrolling(BaseAIBots botTransform)
    {
        Vector3 moveDirection = (_currentDestination - botTransform.transform.position).normalized;
        botTransform.transform.position = 
            Vector3.MoveTowards(botTransform.transform.position, 
                _currentDestination, botTransform.PatrollingStateRunSpeed * Time.deltaTime);
        
        if (moveDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(_currentDestination - botTransform.transform.position, Vector3.up);
            botTransform.transform.rotation = rotation;
        }
        
        if (Vector3.Distance(botTransform.transform.position, _currentDestination) < 0.1f)
        {
            SetRandomDestination(botTransform);
        }
    }
    
    private void SetRandomDestination(BaseAIBots botTransform)
    {
        float randomAngle = Random.Range(0f, 360f);
        float angleInRadians = randomAngle * Mathf.Deg2Rad;
        
        float x = _initialPosition.x + _patrolRadius * Mathf.Cos(angleInRadians);
        float z = _initialPosition.z + _patrolRadius * Mathf.Sin(angleInRadians);

        _currentDestination = new Vector3(x, botTransform.transform.position.y, z);
    }
    
    /*if (Physics.Raycast(_walkPoint, -botTransform.transform.up, 10f, botTransform.groundLayer))
    {
        _walkPointSet = true;
    }
    */
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}