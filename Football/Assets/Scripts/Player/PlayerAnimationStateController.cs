using System;
using UnityEngine;

public class PlayerAnimationStateController : MonoBehaviour
{
    public static Animator AnimatorController { get; set; }
    
    public static int IsRunningWithOutBallHash;
    public static int IsRunningWithBallHash;
    public static int IsPassingBallHash;

    private void Start()
    {
        AnimatorController = GetComponent<Animator>();
        IsRunningWithOutBallHash = Animator.StringToHash("isRunningWithOutBall");
        IsRunningWithBallHash = Animator.StringToHash("isRunningWithBall");
        IsPassingBallHash = Animator.StringToHash("isPassing");
    }
}
