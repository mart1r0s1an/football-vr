using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject _spawnedPlayerPrefab;
    private GameObject _ballPrefab;

    [SerializeField] private GameObject playerPrefab;
    private GameObject _player;

    private GameObject _goalKeeper;
    private GameObject _goalKeeperOpponent;
    
    private GameObject _playerForwardLeft;
    private GameObject _playerForwardRight;
    private GameObject _playerDefenderLeft;
    private GameObject _playerDefenderRight;
    
    private GameObject _playerForwardLeftOpponent;
    private GameObject _playerForwardRightOpponent;
    private GameObject _playerDefenderLeftOpponent;
    private GameObject _playerDefenderRightOpponent;
    

    [SerializeField] private Vector3 ballPosition;
    
    [Header("All players positions")]
    [SerializeField] private List<Vector3> ourPlayersPositions = new List<Vector3>();
    [SerializeField] private List<Vector3> opponentPlayersPositions = new List<Vector3>();
    [SerializeField] private List<Vector3> goalKeepersPositions = new List<Vector3>();        
    
    [Header("All players")]
    [SerializeField] private List<GameObject> ourPlayers = new List<GameObject>();
    [SerializeField] private List<GameObject> opponentPlayers = new List<GameObject>();
    
    
    
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        
        _player = PhotonNetwork.Instantiate(playerPrefab.name, playerPrefab.transform.position, playerPrefab.transform.rotation);

        
        if (PhotonNetwork.IsMasterClient)
        {
            _ballPrefab = PhotonNetwork.Instantiate("Ball", ballPosition, Quaternion.identity);
            
            _playerForwardLeft = PhotonNetwork.Instantiate("Bot Forward Left", ourPlayersPositions[0], Quaternion.identity);
            ourPlayers.Add(_playerForwardLeft);
            _playerForwardRight = PhotonNetwork.Instantiate("Bot Forward Right", ourPlayersPositions[1], Quaternion.identity);
            ourPlayers.Add(_playerForwardRight);
            _playerDefenderLeft = PhotonNetwork.Instantiate("Bot Left Defender", ourPlayersPositions[2], Quaternion.identity);
            ourPlayers.Add(_playerDefenderLeft);
            _playerDefenderRight = PhotonNetwork.Instantiate("Bot Right Defender", ourPlayersPositions[3], Quaternion.identity);
            ourPlayers.Add(_playerDefenderRight);
            _goalKeeper = PhotonNetwork.Instantiate("Goalkeeper", goalKeepersPositions[0], Quaternion.Euler(0f,0f,0f));
            _goalKeeper.tag = "Our Goalkeeper";
            ourPlayers.Add(_goalKeeper);
            
            
            _playerForwardLeftOpponent = PhotonNetwork.Instantiate("Bot Forward Left Opponent", opponentPlayersPositions[0], Quaternion.identity);
            opponentPlayers.Add(_playerForwardLeftOpponent);
            _playerForwardRightOpponent = PhotonNetwork.Instantiate("Bot Forward Right Opponent", opponentPlayersPositions[1], Quaternion.identity);
            opponentPlayers.Add(_playerForwardRightOpponent);
            _playerDefenderLeftOpponent = PhotonNetwork.Instantiate("Bot Left Defender Opponent", opponentPlayersPositions[2], Quaternion.identity);
            opponentPlayers.Add(_playerDefenderLeftOpponent);
            _playerDefenderRightOpponent = PhotonNetwork.Instantiate("Bot Right Defender Opponent", opponentPlayersPositions[3], Quaternion.identity);
            opponentPlayers.Add(_playerDefenderRightOpponent);
            _goalKeeperOpponent = PhotonNetwork.Instantiate("Goalkeeper", goalKeepersPositions[1], Quaternion.Euler(0f,180f,0f));
            _goalKeeperOpponent.tag = "Opponent Goalkeeper";
            opponentPlayers.Add(_goalKeeperOpponent);
        }
        else
        {
            return;
        }
        
        foreach (StateController player in FindObjectsOfType(typeof(StateController)))
        {
            if(player.CompareTag("Bot"))
                GameManager.Instance.ourPlayers.Add(player);
            
            else if (player.CompareTag("BotOpponent"))
                GameManager.Instance.opponentPlayers.Add(player);
        }
    }
    

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.DestroyAll(true);
    }
}
