using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static Dictionary<Enum, Action<float>> _eventDictionary = new Dictionary<Enum, Action<float>>();
    private static Dictionary<Enum, Action> _eventDictionaryWithOutArgument = new Dictionary<Enum, Action>();
    
    public static event Action<float> OnPlayerScored;
    public static event Action<float> OnPlayerKicked;

    private void Start()
    {
        _eventDictionary.Add(PlayerState.Scored, OnPlayerScored);
        _eventDictionary.Add(PlayerState.Kicked, OnPlayerKicked);
    }
    

    public static void CallBackEventWithArgument(PlayerState state, float parameter)
    {
        if (_eventDictionary.ContainsKey(state))
        {
            if (_eventDictionary.TryGetValue(state, out var action))
            {
                action?.Invoke(parameter);
            }
        }
    }

    public static void CallBackEvent(PlayerState state)
    {
        if (_eventDictionaryWithOutArgument.ContainsKey(state))
        {
            if (_eventDictionaryWithOutArgument.TryGetValue(state, out var action))
            {
                action?.Invoke();
            }
        }
    }

    private void OnDisable()
    {
        _eventDictionary.Remove(PlayerState.Scored);
        _eventDictionary.Remove(PlayerState.Kicked);
    }
}

public enum PlayerState
{
    Scored,
    Kicked,
}