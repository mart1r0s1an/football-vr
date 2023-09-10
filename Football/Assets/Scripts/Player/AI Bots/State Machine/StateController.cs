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
    
    #endregion
    
    private IPlayerState _currentState;
    private BaseAIBots _aiBots;
    private CharacterController _characterController;
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _aiBots = GetComponent<BaseAIBots>();
        
        
        ChangeState(standingState);
    }
    
    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnUpdate(this, _characterController, _aiBots);
        }
    }
    
    public void ChangeState(IPlayerState newState)
    {
        if (_currentState != null)
        {
            _currentState.OnExit(this, _aiBots);
        }
        _currentState = newState;
        _currentState.OnEnter(this, _aiBots);
    }
}

public interface IPlayerState
{
    public void OnEnter(StateController stateController, BaseAIBots baseAIBots);

    public void OnUpdate(StateController stateController, CharacterController characterController, BaseAIBots baseAIBots);
    
    public void OnExit(StateController stateController, BaseAIBots baseAIBots);
}