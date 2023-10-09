using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI roomName;

    public void OnClickCreateRoom()
    {
        if(!PhotonNetwork.IsConnected)
            return;
        
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        PhotonNetwork.JoinOrCreateRoom(roomName.ToString(), options, TypedLobby.Default);
        
    }

    public override void OnCreatedRoom()
    {
        print("Created room successfully. ");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Room creation failed. " + message);
    }
}
