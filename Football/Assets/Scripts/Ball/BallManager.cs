using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BallManager : MonoBehaviourPunCallbacks
{
    public static BallManager Instance { get; private set; }
    
    private Transform _ball;
    private Transform _closestPlayer;
    private Transform _localClosestPlayer;
    private Rigidbody _ballRigidbody;
    private Transform _attachedPlayer;
    
    private bool _ballAttached;
    
    public bool BallAttached
    {
        get
        {
            return _ballAttached;
        }
        set
        {
            _ballAttached = value;
        }
    }
    
    [SerializeField] private float attachDistance = 1.5f;
    
    private Vector3 _speedBall;
    private Vector3 _previousPositionBall;
    
    [SerializeField] private PhotonView photonView;
    
    private void Awake()
    {
        _ballRigidbody = GetComponentInChildren<Rigidbody>();
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
            return;
        } 
        else 
        { 
            Instance = this; 
        } 
        
        _ball = this.transform;
    }
    
    private void Update()
    {
        UpdateBallSpeedAndRotation();
    }
    
    public void KickTheBall(float kickForce)
    {
        GameObject goalKeeper = GameObject.Find("Goalkeeper(Clone)");
        goalKeeper.GetComponent<GoalkeeperAI>().AnimationStateCanChange = true;
        
        _ballRigidbody.isKinematic = false;
        _ball.parent = null;
        
        Vector3 curveDirection = new Vector3(Random.Range(-.5f, .5f), Random.Range(.3f, .5f), 0f);
        Vector3 throwDirection = _attachedPlayer.transform.forward + curveDirection;
        _ballRigidbody.AddForce(throwDirection * kickForce);
        _ballAttached = false;
        attachDistance = 0;
        
        StartCoroutine(ChangeAttachDistance());
    }

    public void PassTheBall(float passingForce)
    {
        _ballRigidbody.isKinematic = false;
        _ball.parent = null;
        
        Vector3 throwDirection = _attachedPlayer.transform.forward;
        _ballRigidbody.AddForce(throwDirection * passingForce);
        _ballAttached = false;
        attachDistance = 0;
        
        StartCoroutine(ChangeAttachDistance());
    }
    

    public void AttachBall(Transform player)
    {
        _ball.SetParent(player);
        player.GetComponent<BaseAIBots>().HasBall = true;
        _ballAttached = true;
        _attachedPlayer = player;
        _ballRigidbody.isKinematic = true;
    }
    
    public void DetachBall(Transform player)
    {
        player.GetComponent<PlayerBase>().HasBall = false;
        _ball.SetParent(null);
        attachDistance = 0;
        
        _ballAttached = false;
        
        _attachedPlayer = null;
        
        _ball.SetParent(null);
        _ballRigidbody.isKinematic = false;
        
    }
    
    
    private void UpdateBallSpeedAndRotation()
    {
        if (Time.deltaTime > 0)
        {
            _speedBall = new Vector3((transform.position.x - _previousPositionBall.x) / Time.deltaTime, 0, (transform.position.z - _previousPositionBall.z) / Time.deltaTime);
        }
        
        _previousPositionBall.x = transform.position.x;
        _previousPositionBall.z = transform.position.z;
        Vector3 rotationAxis = Vector3.Cross(_speedBall.normalized, Vector3.up);
        transform.Rotate(rotationAxis, -_speedBall.magnitude * 1.8f, Space.World);
    }
    
    private IEnumerator ChangeAttachDistance()
    {
        yield return new WaitForSeconds(1);

        attachDistance = 3;
    }

    private void OnDisable()
    {
        _ballAttached = false;
    }
}