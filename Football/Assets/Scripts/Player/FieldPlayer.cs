using System.Collections;
using UnityEngine;

public class FieldPlayer : PlayerBase
{
    private bool _sliderDown;
    public bool sliderDown
    {
        get
        {
            return _sliderDown;
        }
        set
        {
            _sliderDown = value;
        }
    }
    
    private void OnEnable()
    {
        EventManager.OnPlayerKicked += BallKick;
        
        GetDataForPlayer();
    }

    private void Update()
    {
        AnimationState();
        Input();
        CheckInput();
    }

    private void FixedUpdate()
    {
        Movement();
        GroundCheck();
        Gravity();
    }

    protected override void GetDataForPlayer()
    {
        KickForce = PlayerScriptable.KickForce;
        JumpForce = PlayerScriptable.JumpForce;
        PlayerSpeed = PlayerScriptable.PlayerSpeed;
        gravity = PlayerScriptable.Gravity;
    }
    
    
    protected override void Movement()
    {
        Vector3 movement = new Vector3(-_vertical, 0f, _horizontal);

        playerCharacterController.Move(movement * PlayerSpeed * Time.deltaTime);


        if (_horizontal > 0 || _vertical > 0 || _horizontal < 0 || _vertical < 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360);
        }
    }

    protected override void BallKick(float kickForce)
    {
        if (BallManager.Instance.BallAttached)
        {
            BallManager.Instance.KickTheBall(kickForce);
        }

        KickTheBall = false;
    }
    

    private void CheckInput()
    {
        if (UnityEngine.Input.GetKey(KeyCode.C) && KickTheBall && _vertical == 0 && _horizontal == 0)
        {
            if (KickForce <= 150)
            {
                KickForce += 1;
                UIManager.Instance.SetSlider(KickForce);
            }

            else
            {
                return;
            }
        }
        
        if (UnityEngine.Input.GetKeyUp(KeyCode.C) && KickTheBall && _vertical == 0 && _horizontal == 0)
        {
            _sliderDown = true;
            
            PlayerAnimationStateController.AnimatorController.SetBool(
                PlayerAnimationStateController.IsRunningWithOutBallHash, false);
            PlayerAnimationStateController.AnimatorController.SetBool(
                PlayerAnimationStateController.IsRunningWithBallHash, false);
            PlayerAnimationStateController.AnimatorController.SetBool(PlayerAnimationStateController.IsPassingBallHash,
                true);
            

            StartCoroutine(CallPassEvent(KickForce));
            StartCoroutine(CallAnimationStateChange(1.7f, PlayerAnimationStateController.IsPassingBallHash, false));
        }
    }


    private IEnumerator CallPassEvent(float ballKickForce)
    {
        yield return new WaitForSeconds(0.4f);

        EventManager.CallBackEventWithArgument(PlayerState.Kicked, ballKickForce);

        KickForce = 30;
    }

    protected override void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(checkGroundPosition.position, checkGroundPositionRadius, checkGroundLayer);

        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }

    protected override void Gravity()
    {
        _velocity.y += gravity * Time.deltaTime;

        playerCharacterController.Move(_velocity * Time.deltaTime);
    }

    protected override void AnimationState()
    {
        if (_horizontal != 0 || _vertical != 0)
            PlayerAnimationStateController.AnimatorController.SetBool(
                PlayerAnimationStateController.IsRunningWithOutBallHash, true);

        if (_horizontal == 0 && _vertical == 0)
        {
            PlayerAnimationStateController.AnimatorController.SetBool(
                PlayerAnimationStateController.IsRunningWithOutBallHash, false);
            PlayerAnimationStateController.AnimatorController.SetBool(
                PlayerAnimationStateController.IsRunningWithBallHash, false);
        }

        if (_horizontal == 0 && _vertical == 0 && BallManager.Instance.BallAttached)
        {
            PlayerAnimationStateController.AnimatorController.SetBool(
                PlayerAnimationStateController.IsRunningWithBallHash, false);
        }

        if (BallManager.Instance.BallAttached && (_horizontal != 0 || _vertical != 0))
        {
            PlayerAnimationStateController.AnimatorController.SetBool(
                PlayerAnimationStateController.IsRunningWithOutBallHash, false);
            PlayerAnimationStateController.AnimatorController.SetBool(
                PlayerAnimationStateController.IsRunningWithBallHash, true);

            /*if (UnityEngine.Input.GetKeyDown(KeyCode.C) && KickTheBall)
            {
                PlayerAnimationStateController.AnimatorController.SetBool(PlayerAnimationStateController.IsRunningWithOutBallHash, false);
                PlayerAnimationStateController.AnimatorController.SetBool(PlayerAnimationStateController.IsRunningWithBallHash, false);
            }*/
        }
        else
        {
            PlayerAnimationStateController.AnimatorController.SetBool(
                PlayerAnimationStateController.IsRunningWithBallHash, false);
        }
    }

    private IEnumerator CallAnimationStateChange(float time, int animationNameHash, bool state)
    {
        yield return new WaitForSeconds(time);

        PlayerAnimationStateController.AnimatorController.SetBool(animationNameHash, state);
    }

    private void OnDisable()
    {
        EventManager.OnPlayerKicked -= BallKick;
    }
}