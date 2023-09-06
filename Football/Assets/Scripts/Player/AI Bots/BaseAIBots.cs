using Scriptable;
using UnityEngine;

public class BaseAIBots : PlayerBase
{
    [Space(10f)]
    [SerializeField] private AIBotScriptable aIBotScriptable;

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
