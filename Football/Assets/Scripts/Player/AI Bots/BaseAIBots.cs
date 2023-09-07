using System;
using Scriptable;
using UnityEngine;

public class BaseAIBots : PlayerBase
{
    [Space(20f)]
    [SerializeField] private AIBotScriptable aIBotScriptable;

    [Space(10f)] [Header("Ball")] 
    public Transform Ball;
    
    [Space(10f)]
    [Header("Bots type")]
    public BotType BotType;
    
    
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

    public float RunSpeed
    {
        get { return aIBotScriptable.BotSpeed; }
        set { aIBotScriptable.BotSpeed = value; }
    }

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
