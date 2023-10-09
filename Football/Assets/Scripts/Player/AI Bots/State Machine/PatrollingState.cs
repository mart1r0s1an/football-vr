using UnityEngine;

public class PatrollingState : IPlayerState
{
    private bool _walkPointSet;
    private Vector3 _walkPoint;
    
    private float _shortestDistance;
    private float _canPassTheBallDistance;
    private float _timeBoolSetTrue = 3f;
    
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        _shortestDistance = stateController.AttackToTheAttackerWithTheBall;
    }

    public void OnUpdate(StateController stateController, BaseAIBots baseAIBots)
    {
        if (stateController.CompareTag("Bot"))
            Patrolling(baseAIBots, 15);
        else if (stateController.CompareTag("BotOpponent"))
            Patrolling(baseAIBots, 190);
        
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
    
    private void Patrolling(BaseAIBots botTransform, float radius)
    {
        if (!_walkPointSet) 
            SearchWalkPoint(botTransform, radius);
        
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

    private void SearchWalkPoint(BaseAIBots botTransform, float radius)
    {
        /*float randomZ = Random.Range(-range, range);
        float randomX = Random.Range(-range, range);
        
        _walkPoint = new Vector3(botTransform.transform.position.x + randomX, 
            botTransform.transform.position.y, botTransform.transform.position.z + randomZ);
        
        if (Physics.Raycast(_walkPoint, -botTransform.transform.up, 2f, botTransform.groundLayer))
        {
            _walkPointSet = true;   
        }*/
        
        // Calculate a random angle in degrees
        float randomAngle = Random.Range(0f, 360f);

        // Convert the angle to radians
        float angleInRadians = randomAngle * Mathf.Deg2Rad;

        // Calculate the random position within the circle
        float x = radius * Mathf.Cos(angleInRadians);
        float z = radius * Mathf.Sin(angleInRadians);

        // Set the _walkPoint to the new position within the circle centered around the player
        _walkPoint = new Vector3(botTransform.transform.position.x + x, 
            botTransform.transform.position.y, 
            botTransform.transform.position.z + z);

        // Perform a raycast to ensure the new position is on the ground
        if (Physics.Raycast(_walkPoint, -botTransform.transform.up, 2f, botTransform.groundLayer))
        {
            _walkPointSet = true;   
        }
    }
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}