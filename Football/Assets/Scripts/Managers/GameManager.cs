using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<PlayerBase> allPlayers = new List<PlayerBase>();
    
    private void Awake()
    {
        foreach (PlayerBase player in FindObjectsOfType(typeof(PlayerBase)))
        {
            allPlayers.Add(player);
        }
    }
    
    private void OnEnable()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
}
