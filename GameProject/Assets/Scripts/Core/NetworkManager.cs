using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    private List<RoomInfo> roomList = new List<RoomInfo>();
    private bool inLobby;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // 포톤 클라우드 접속
        // PhotonNetwork.ConnectUsingSettings();
    }

    public int GetRoomsCount()
    {
        return PhotonNetwork.CountOfRooms;
    }

    public void CreateRoom(string roomName, int maxPlayers)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(roomName, options);
    }

    public IEnumerator JoinGame()
    {
        int count = 0;
        while (!inLobby)
        {
            count++;
            yield return StartCoroutine(TryJoinLobby());
            if (count < 3)
                break;
        }
    }

    public bool CheckInLobby()
    {
        return inLobby;
    }

    public bool JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        if (PhotonNetwork.InRoom)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool JoinRoom(string roomName)
    {
        return PhotonNetwork.JoinRoom(roomName);
    }

    public List<RoomList> GetRoomList()
    {
        List<RoomList> _roomList = new List<RoomList>();
        foreach( RoomInfo _roomInfo in roomList )
        {
            RoomList _room = new RoomList();
            _room.roomName = _roomInfo.Name;
            _room.maxPlayers = _roomInfo.MaxPlayers;
            _room.playerNum = _roomInfo.PlayerCount;
            _roomList.Add(_room);
        }

        return _roomList;
    }

    private IEnumerator TryJoinLobby()
    {
        if ( !PhotonNetwork.IsConnectedAndReady )
        {
            Debug.Log("포톤 접속 시도");
            PhotonNetwork.ConnectUsingSettings();
            yield return new WaitForSeconds(0.5f);
        }
        else if (!inLobby )
        {
            Debug.Log("로비 접속 시도");
            PhotonNetwork.JoinLobby();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        inLobby = true;
    }

    public override void OnLeftLobby()
    {
        base.OnLeftLobby();
        inLobby = false;
    }

    public override void OnRoomListUpdate(List<RoomInfo> _roomList)
    {
        base.OnRoomListUpdate(_roomList);
        roomList = _roomList;
    }
}
