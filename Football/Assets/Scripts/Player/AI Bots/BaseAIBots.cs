using Scriptable;
using UnityEngine;

public class BaseAIBots : PlayerBase
{
    [Space(20f)]
    [SerializeField] private AIBotScriptable aIBotScriptable;

    [Space(10f)] 
    [Header("Ball and Goals")]
    public Transform FirstHalfGoalPosition;
    public Transform SecondHalfGoalPosition;
    public Transform Ball;
    
    [Space(10f)]
    [Header("Bots type")]
    public BotType BotType;

    public LayerMask groundLayer;
    
    
    [Space(20f)]
    [Header("Start Bots Positions")]
    #region StartPositions

    public Transform ForwardRightStartPosition;
    public Transform ForwardLeftStartPosition;
    public Transform DefenderRightBackStartPosition;
    public Transform DefenderLeftBackStartPosition;
    public Transform GoalkeeperStartPosition;
    
    #endregion
    
    private float _kickForce;

    public float KickBallForce
    {
        get
        {
            return _kickForce;
        } 
        private set
        {
            _kickForce = value;
        }
    }

    public float RunSpeed
    {
        get { return aIBotScriptable.BotSpeed; }
        set { aIBotScriptable.BotSpeed = value; }
    }
    
    public float PatrollingStateRunSpeed
    {
        get { return aIBotScriptable.PatrollingRunSpeed; }
        set { aIBotScriptable.PatrollingRunSpeed = value; }
    }
    
    #region BotAnimtion

    [Space(20f)]
    [Header("Animation")]
    public Animator BotAnimatorController;
    [HideInInspector] public int IsRunningWithOutBallHash;
    [HideInInspector] public int IsRunningWithBallHash;
    [HideInInspector] public int IsPassingBallHash;

    #endregion

    private void Awake()
    {
        IsRunningWithOutBallHash = Animator.StringToHash("isRunningWithOutBall");
        IsRunningWithBallHash = Animator.StringToHash("isRunningWithBall");
        IsPassingBallHash = Animator.StringToHash("isPassing");
        
        _kickForce = aIBotScriptable.KickForce;
        
        switch (BotType)
        {
            case BotType.ForwardLeft:
                transform.position = ForwardLeftStartPosition.position;
                break;

            case BotType.ForwardRight:
                transform.position = ForwardRightStartPosition.position;
                break;
            
            case BotType.DefenderLeftBack:
                transform.position = DefenderLeftBackStartPosition.position;
                break;
            
            case BotType.DefenderRightBack:
                transform.position = DefenderRightBackStartPosition.position;
                break;
        }
    }
}

public enum BotType
{
    ForwardRight,
    ForwardLeft, 
    DefenderRightBack,
    DefenderLeftBack,
    Goalkeeper
}
