using System.Collections.Generic;
using UnityEngine;

public class PatrollingState : IPlayerState
{
    private bool _walkPointSet;
    private Vector3 _walkPoint;
    private Transform _closestLocalPlayer;
    private List<PlayerBase> _players = new List<PlayerBase>();
    
    
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots)
    {
        _players = GameManager.Instance.allPlayers;
        
        Debug.Log("hello from patrolling state");
    }

    public void OnUpdate(StateController stateController, CharacterController characterController, BaseAIBots baseAIBots)
    {
        Patroling(baseAIBots);
        
        GetClosestPlayer(stateController, baseAIBots);
        GetFreeBall(stateController, baseAIBots);
    }

    private void GetFreeBall(StateController stateController, BaseAIBots baseAIBots)
    {
        //find ball if it is free and attack to the ball
        
        Transform ball = null;
        float shortestDistance = 20f;
        
        float distance = Vector3.Distance(baseAIBots.transform.position, BallManager.Instance.transform.position);
            
        if (distance < shortestDistance)
        {
            ball = BallManager.Instance.transform;

            if (ball.parent == null)
            {
                stateController.ChangeState(stateController.runningState, 0);
                Debug.Log("I can get the ball right now mother fukcer");   
            }
        }
    }

    private Transform GetClosestPlayer(StateController stateController, BaseAIBots baseAIBots)
    {
        _closestLocalPlayer = null;
        float shortestDistance = Mathf.Infinity;
        
        foreach (PlayerBase player in _players)
        {
            float distance = Vector3.Distance(player.transform.position, BallManager.Instance.transform.position);
            
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                _closestLocalPlayer = player.transform;
            }
        }

        if (_closestLocalPlayer.CompareTag("Bot") && _closestLocalPlayer.GetComponent<PlayerBase>().HasBall)
        {
            switch (baseAIBots.BotType)
            {
                case BotType.ForwardLeft:
                    break;
            
                case BotType.ForwardRight:
                
                    break;
            
                case BotType.DefenderLeftBack:
               
                
                    break;
            
                case BotType.DefenderRightBack:
                    baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithBallHash, true);
                
                    Vector3 movedirection = _closestLocalPlayer.transform.position
                                            - new Vector3(baseAIBots.transform.position.x, 0, baseAIBots.transform.position.z);
        
                    float distanceToPlayer = movedirection.magnitude;
        
                    Vector3 moveSpeed = new Vector3(movedirection.normalized.x * baseAIBots.RunSpeed * Time.deltaTime, 0,
                        movedirection.normalized.z * baseAIBots.RunSpeed * 3 * Time.deltaTime);
        
                    baseAIBots.transform.position += moveSpeed;
                
                    baseAIBots.transform.LookAt(_closestLocalPlayer.transform);
                    
                    
                    if (distanceToPlayer <= 2f && _closestLocalPlayer.GetComponent<PlayerBase>().HasBall)
                    {
                        BallManager.Instance.DetachBallWithSomeForce(_closestLocalPlayer);
                        //stateController.ChangeState(stateController.);
                        
                        
                        //BallManager.Instance.AttachBall(_localClosestPlayer);
                        
                        baseAIBots.BotAnimatorController.SetBool(baseAIBots.IsRunningWithOutBallHash, true);
                    }
                    break;
            }
        }
        
        else if (!_closestLocalPlayer.GetComponent<PlayerBase>().HasBall)
        {
            Debug.Log("Do not attack");
            return null;
        }
        
        return _closestLocalPlayer;
    }
    
    private void Patroling(BaseAIBots botTransform)
    {
        if (!_walkPointSet) SearchWalkPoint(botTransform);

        Vector3 movedirection = _walkPoint - new Vector3(botTransform.transform.position.x, 0, botTransform.transform.position.z);
        
        Vector3 moveSpeed = new Vector3(movedirection.normalized.x * botTransform.PatrollingStateRunSpeed * Time.deltaTime, 0,
            movedirection.normalized.z * botTransform.PatrollingStateRunSpeed * Time.deltaTime);

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
        float randomZ = Random.Range(-10, 10);
        float randomX = Random.Range(-10, 10);

        _walkPoint = new Vector3(botTransform.transform.position.x + randomX, botTransform.transform.position.y, botTransform.transform.position.z + randomZ);

        if (Physics.Raycast(_walkPoint, -botTransform.transform.up, 2f))
            _walkPointSet = true;
    }
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots)
    {
        
    }
}
