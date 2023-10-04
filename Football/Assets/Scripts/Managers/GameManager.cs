using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public List<StateController> ourPlayers = new List<StateController>();
    public List<StateController> opponentPlayers = new List<StateController>();

    [SerializeField] private Rigidbody ball;
    
    [SerializeField] private List<Transform> ourPlayersPositions = new List<Transform>();
    [SerializeField] private List<Transform> opponentPlayersPositions = new List<Transform>();
    
    
    private void Awake()
    {
        foreach (StateController player in FindObjectsOfType(typeof(StateController)))
        {
            if(player.CompareTag("Bot"))
                ourPlayers.Add(player);
            
            else if (player.CompareTag("BotOpponent"))
                opponentPlayers.Add(player);
        }
    }

    private void OnEnable()
    {
        ResetTheGame();
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ResetTheGame();
        }
    }

    public void ResetTheGame()
    {
        ball.velocity = Vector3.zero;
        ball.position = new Vector3(0f, 0f, 0f);
        
        for (int i = 0; i < ourPlayers.Count; i++)
        {
            ourPlayers[i].transform.position = ourPlayersPositions[i].transform.position;
        }
        
        for (int i = 0; i < opponentPlayers.Count; i++)
        {
            opponentPlayers[i].transform.position = opponentPlayersPositions[i].transform.position;
        }
    }
}
