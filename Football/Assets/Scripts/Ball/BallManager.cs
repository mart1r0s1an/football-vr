using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance { get; private set; }
    
    private Transform _ball;
    private Transform _closestPlayer;
    private Transform _localClosestPlayer;
    private Rigidbody _ballRigidbody;
    private Transform _attachedPlayer;
    private List<PlayerBase> _players = new List<PlayerBase>();

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


    [SerializeField] private float curvedAmount;
    [SerializeField] private float attachDistance = 1.5f;
    
    private Vector3 _speedBall;
    private Vector3 _previousPositionBall;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
        
        _ball = this.transform;
    }

    private void Start()
    {
        
        _ballRigidbody = GetComponentInChildren<Rigidbody>();

        _players = GameManager.Instance.allPlayers;
    }
    
    
    private void Update()
    {
        _closestPlayer = GetClosestPlayer();
        
        
        if (_attachedPlayer == null && Vector3.Distance(_closestPlayer.position, _ball.position) <= attachDistance)
        {
            AttachBall(_closestPlayer);
           
            _ball.localPosition = new Vector3(0f, 0.1f,0.4f);
        }
        
        else if (_attachedPlayer == _closestPlayer && Vector3.Distance(_closestPlayer.position, _ball.position) > attachDistance)
        {
            DetachBall();
        }
        
        UpdateBallSpeedAndRotation();
    }

    private Transform GetClosestPlayer()
    {
        _localClosestPlayer = null;
        float shortestDistance = Mathf.Infinity;
        
        foreach (PlayerBase player in _players)
        {
            float distance = Vector3.Distance(player.transform.position, _ball.position);
            
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                _localClosestPlayer = player.transform;
            }
        }

        return _localClosestPlayer;
    }

    public void KickTheBall(float kickForce)
    {
        Debug.Log("Kick");
        
        attachDistance = 0;
        _ballAttached = false;
        _ball.SetParent(null);

        _ballRigidbody.isKinematic = false;
        
        Vector3 curveDirection = new Vector3(0f, curvedAmount, 0f);
        Vector3 throwDirection = _attachedPlayer.transform.forward + curveDirection;
        
        _ballRigidbody.AddForce(throwDirection * kickForce);
        
        _attachedPlayer = null;
        
        StartCoroutine(ChangeAttachDistance());
    }

    private void AttachBall(Transform player)
    {
        _ballAttached = true;
        _attachedPlayer = player;
        player.GetComponent<PlayerBase>().KickTheBall = true;   
        
        _ballRigidbody.isKinematic = true;
        _ball.SetParent(player);
    }
    
    private void DetachBall()
    {
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