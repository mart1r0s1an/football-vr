using Scriptable;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    #region Field

    private string _horizontalInput = "Horizontal";
    private string _verticalInput = "Vertical";
    
    protected float _horizontal;
    protected float _vertical;
    protected Vector3 _velocity;


    [Header("Player")] 
    public PlayerScriptable PlayerScriptable;

    private bool _kickTheBall;
    public bool KickTheBall
    {
        get
        {
            return _kickTheBall;
        }
        
        set
        {
            _kickTheBall = value;
        }
    }

    private bool _hasBall;
    
    public bool HasBall
    {
        get
        {
            return _hasBall;
        }
        set
        {
            _hasBall = value;
        }
    }
    
    [SerializeField] protected CharacterController playerCharacterController;
    protected float PlayerSpeed;
    protected float KickForce;
    protected float gravity;
    
    #endregion


    protected virtual void GetDataForPlayer()
    {
        KickForce = PlayerScriptable.KickForce;
        PlayerSpeed = PlayerScriptable.PlayerSpeed;
        gravity = PlayerScriptable.Gravity;
    }
    
    
    protected virtual void Input()
    {
        _horizontal = UnityEngine.Input.GetAxis(_horizontalInput);
        _vertical = UnityEngine.Input.GetAxis(_verticalInput);
    }

    protected virtual void Movement()
    {
        
    }

    protected virtual void BallKick(float kickForce)
    {
        
    }

    protected virtual void GroundCheck()
    {
        
    }

    protected virtual void Gravity()
    {
        
    }

    protected virtual void AnimationState()
    {
        
    }
    
}
