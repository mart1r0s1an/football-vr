using UnityEngine;

public class StateController : MonoBehaviour
{
    private IPlayerState _currentState;
    
    public StandingState standingState = new StandingState();
    public RunningState runningState = new RunningState();
    public PassingState passingStat = new PassingState();
    public KickingState kickingState = new KickingState();
    public ReceivingState receivingState = new ReceivingState();

    private void Start()
    {
        ChangeState(standingState);
    }
    
    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnUpdate(this);
        }
    }
    
    public void ChangeState(IPlayerState newState)
    {
        if (_currentState != null)
        {
            _currentState.OnExit(this);
        }
        _currentState = newState;
        _currentState.OnEnter(this);
    }
}

public interface IPlayerState
{
    public void OnEnter(StateController state);

    public void OnUpdate(StateController state);
    
    /*public void OnStandingState(StateController state);
    public void OnRunningState(StateController state);
    public void OnPassingState(StateController state);
    public void OnKickingState(StateController state);
    public void OnReceivingState(StateController state);*/
    
    public void OnExit(StateController state);
}