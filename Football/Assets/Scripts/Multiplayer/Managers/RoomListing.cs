using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomListing : MonoBehaviourPunCallbacks
{
   [SerializeField] private TextMeshProUGUI roomName;

   public RoomInfo RoomInfo { get; private set; }
   
   public void SetRoomInfo(RoomInfo roomInfo)
   {
       RoomInfo = roomInfo;
       roomName.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
   }
}
