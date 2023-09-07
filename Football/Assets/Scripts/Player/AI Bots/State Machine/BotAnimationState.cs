using UnityEngine;

public class BotAnimationState : MonoBehaviour
{
    public static Animator BotAnimatorController { get; set; }
    
    public static int IsRunningWithOutBallHash;
    public static int IsRunningWithBallHash;
    public static int IsPassingBallHash;

    private void Start()
    {
        BotAnimatorController = GetComponent<Animator>();
        IsRunningWithOutBallHash = Animator.StringToHash("isRunningWithOutBall");
        IsRunningWithBallHash = Animator.StringToHash("isRunningWithBall");
        IsPassingBallHash = Animator.StringToHash("isPassing");
    }
}
