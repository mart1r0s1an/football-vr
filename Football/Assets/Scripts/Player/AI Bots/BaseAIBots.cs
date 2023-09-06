using Scriptable;
using UnityEngine;

public class BaseAIBots : PlayerBase
{
    [SerializeField] private AIBotScriptable aIBotScriptable;

    private float _kickForce;
    
    private void Start()
    {
        _kickForce = aIBotScriptable.KickForce;

        
        
        Debug.Log(_kickForce);
    }
}
