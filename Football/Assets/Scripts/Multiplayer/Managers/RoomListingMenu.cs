using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomListing roomListingPrefab;
    [SerializeField] private Transform content;

    private List<RoomListing> _roomListings = new List<RoomListing>();
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = _roomListings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(_roomListings[index].gameObject);
                    _roomListings.RemoveAt(index);
                }
            }
            else
            {
                RoomListing roomListing = Instantiate(roomListingPrefab, content);

                if (roomListing is not null)
                {
                    roomListing.SetRoomInfo(info);
                    _roomListings.Add(roomListing);
                }
            }
        }
    }
}