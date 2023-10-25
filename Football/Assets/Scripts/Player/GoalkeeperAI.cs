using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoalkeeperAI : MonoBehaviour
{
    private Animator _goalKeeperAnimator;
    private GameObject _ourPlayer;
    private GameObject _opponentPlayer;

    public bool AnimationStateCanChange;

    private void Start()
    {
        _goalKeeperAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (AnimationStateCanChange)
        {
            if (Random.Range(0, 2) == 0)
            {
                Debug.Log("jump left");
                _goalKeeperAnimator.SetBool("idle", false);
                _goalKeeperAnimator.SetBool("jumpLeft", true);
            }
            else
            {
                Debug.Log("jump right");
                _goalKeeperAnimator.SetBool("idle", false);
                _goalKeeperAnimator.SetBool("jumpRight", true);
            }

            StartCoroutine(ChangeState());
        }
        else
        {
            _goalKeeperAnimator.SetBool("jumpRight", false);
            _goalKeeperAnimator.SetBool("jumpLeft", false);
            _goalKeeperAnimator.SetBool("idle", true);
        }
    }

    private IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(3);
        AnimationStateCanChange = false;
    }
}
