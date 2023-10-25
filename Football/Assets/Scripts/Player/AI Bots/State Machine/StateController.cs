using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseAIBots))]
public class StateController : MonoBehaviour
{
    #region States

    public StandingState standingState = new StandingState();
    public RunningState runningState = new RunningState();
    public PassingState passingState = new PassingState();
    public KickingState kickingState = new KickingState();
    public ReceivingState receivingState = new ReceivingState();
    public AttackState attackState = new AttackState();
    public PatrollingState patrollingState = new PatrollingState();
    
    #endregion

    #region Distances
    [SerializeField] private float patrollingDistance;
    [SerializeField] private float attackToTheAttackerWithTheBall;
    [SerializeField] private float attackToTheBall;
    [SerializeField] private float canPassTheBallDistance;
    
    
    public float PatrollingDistance
    {
        get
        {
            return patrollingDistance;
        }
        set
        {
            patrollingDistance = value;
        }
    }
    public float AttackToTheAttackerWithTheBall
    {
        get
        {
            return attackToTheAttackerWithTheBall;
        }
        set
        {
            attackToTheAttackerWithTheBall = value;
        }
    }
    
    public Transform ClosestLocalPlayer { get; set; }
    public Transform ClosestLocalOpponent { get; set; }
    public Transform ClosestLocalPlayerToTheGoal { get; set; }
    public bool AlreadyPassed { get; set; }

    #endregion
    
    private IPlayerState _currentState;
    private BaseAIBots _aiBots;
    private List<StateController> _players = new List<StateController>();
    private List<StateController> _opponents = new List<StateController>();
    
    float closestDistance = float.MaxValue;
    
    private void Start()
    {
        ClosestLocalPlayer = null;
        ClosestLocalOpponent = null;
        _players = GameManager.Instance.ourPlayers;
        _opponents = GameManager.Instance.opponentPlayers;
        _aiBots = GetComponent<BaseAIBots>();
        
        ChangeState(standingState, 0f);
    }
    
    private void Update()
    {
        GetClosestPlayerToTheGoal();
        
        GetClosestPlayer();
        
        if (_currentState != null)
        {
            _currentState.OnUpdate(this, _aiBots);
        }
    }

    private void GetClosestPlayer()
    {
        if (CompareTag("Bot"))
        {
            foreach (StateController player in _players)
            {
                var distance = (player.transform.position - transform.position).magnitude;

                if (distance < canPassTheBallDistance && player.CompareTag("Bot") && player != this)
                {
                    ClosestLocalPlayer = player.transform;
                    canPassTheBallDistance = distance;
                }
            }
        }
        else if (CompareTag("BotOpponent"))
        {
            foreach (StateController player in _opponents)
            {
                var distance = (player.transform.position - transform.position).magnitude;

                if (distance < canPassTheBallDistance && player.CompareTag("BotOpponent") && player != this)
                {
                    ClosestLocalOpponent = player.transform;
                    canPassTheBallDistance = distance;
                }
            }
        }
    }
    
    public void ChangeThePassingState()
    {
        Invoke(nameof(ChangeThePassingStateLocal), 2f);
    }

    private void ChangeThePassingStateLocal()
    {
        AlreadyPassed = false;
    }

    private void GetClosestPlayerToTheGoal()
    {
        foreach (StateController player in _players)
        {
            var distance = (player.transform.position - _aiBots.GoalPosition.transform.position).magnitude;

            if (distance < closestDistance && player.CompareTag("Bot") && player != this)
            {
                closestDistance = distance;
                ClosestLocalPlayerToTheGoal = player.transform;
            }
        }

    }
    
    public void ChangeState(IPlayerState newState, float time)
    {
        StartCoroutine(Pause(time));
        
        if (_currentState != null)
        {
            _currentState.OnExit(this, _aiBots);
        }
        _currentState = newState;
        _currentState.OnEnter(this, _aiBots);
    }

    private IEnumerator Pause(float time)
    {
        yield return new WaitForSeconds(time);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrollingDistance);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackToTheAttackerWithTheBall);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackToTheBall);
    }
}

public interface IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots);

    public void OnUpdate(StateController stateController, BaseAIBots baseAIBots);
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots);
}