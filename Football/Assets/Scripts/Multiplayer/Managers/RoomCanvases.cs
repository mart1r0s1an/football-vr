using System;
using UnityEngine;

public class RoomCanvases : MonoBehaviour
{
    [SerializeField] private CreateOrJoinRoomCanvas createOrJoinRoomCanvas;
    public CreateOrJoinRoomCanvas CreateOrJoinRoomCanvas
    {
        get { return createOrJoinRoomCanvas; }
    }
    
    
    [SerializeField] private CurrentRoomCanvas currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas
    {
        get { return currentRoomCanvas; }
    }

    private void Awake()
    {
        FirstInitialize();
    }

    private void FirstInitialize()
    {
        CreateOrJoinRoomCanvas.Instantiate(this);
        CurrentRoomCanvas.Initialize(this);
    }
}
