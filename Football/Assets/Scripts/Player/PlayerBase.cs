using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
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
    
    [SerializeField] protected CharacterController playerCharacterController;
    protected float PlayerSpeed;
    protected float KickForce;
    protected float JumpForce;
    protected float gravity;
    
    [Space(20f)]
    [Header("Ground Check")]
    [SerializeField] protected LayerMask checkGroundLayer;
    [SerializeField] protected Transform checkGroundPosition;
    [SerializeField] protected float checkGroundPositionRadius;
    [SerializeField] protected bool isGrounded;

    
    /*[Header("Mobile Controller")]
    [SerializeField] protected Joystick joystick;*/
    
    #endregion


    protected virtual void GetDataForPlayer()
    {
        KickForce = PlayerScriptable.KickForce;
        JumpForce = PlayerScriptable.JumpForce;
        PlayerSpeed = PlayerScriptable.PlayerSpeed;
        gravity = PlayerScriptable.Gravity;
    }
    
    
    protected virtual void Input()
    {
        /*_horizontal = joystick.Horizontal;//UnityEngine.Input.GetAxis(_horizontalInput);
        _vertical = joystick.Vertical; //UnityEngine.Input.GetAxis(_verticalInput);*/
        
        _horizontal = UnityEngine.Input.GetAxis(_horizontalInput);
        _vertical = UnityEngine.Input.GetAxis(_verticalInput);
    }

    protected abstract void Movement();

    protected abstract void BallKick(float kickForce);

    protected abstract void GroundCheck();
    
    protected abstract void Gravity();

    protected abstract void AnimationState();
    
}