using Scriptable;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseAIBots : PlayerBase
{
    [Space(10f)]
    [SerializeField] private AIBotScriptable aIBotScriptable;

    public BotType BotType;
    
    
    [Space(10f)]
    #region StartPositions

    public Transform ForwardRightStartPosition;
    public Transform ForwardLeftStartPosition;
    public Transform DefenderRightBackStartPosition;
    public Transform DefenderLeftBackStartPosition;
    public Transform GoalkeeperStartPosition;
    
    #endregion
    
    private float _kickForce;
    
    private void Start()
    {
        _kickForce = aIBotScriptable.KickForce;
        
        Debug.Log(_kickForce);
    }
    
    
    protected override void Movement()
    {
        
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
