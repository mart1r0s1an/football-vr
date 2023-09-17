using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BaseAIBots))]
public class StateController : MonoBehaviour
{
    #region States

    public StandingState standingState = new StandingState();
    public RunningState runningState = new RunningState();
    public PassingState passingStat = new PassingState();
    public KickingState kickingState = new KickingState();
    public ReceivingState receivingState = new ReceivingState();
    public AttackState attackState = new AttackState();
    public PatrollingState patrollingState = new PatrollingState();
    
    #endregion
    
    private IPlayerState _currentState;
    private BaseAIBots _aiBots;
    private CharacterController _characterController;
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _aiBots = GetComponent<BaseAIBots>();
        
        ChangeState(standingState, 0f);
    }
    
    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnUpdate(this, _characterController, _aiBots);
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
}

public interface IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots);

    public void OnUpdate(StateController stateController, CharacterController characterController, BaseAIBots baseAIBots);
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots);
}