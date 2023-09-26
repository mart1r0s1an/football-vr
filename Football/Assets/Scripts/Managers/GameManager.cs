using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<StateController> allPlayers = new List<StateController>();
    
    private void Awake()
    {
        foreach (StateController player in FindObjectsOfType(typeof(StateController)))
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
