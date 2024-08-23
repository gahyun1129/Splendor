using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    private List<RoomInfo> roomList = new List<RoomInfo>();

    void Awake()
    {
        instance = this;

        // 씬 전환 시 파괴되지 않도록 설정됨
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        JoinLobby();
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    // 방수 가져오기
    public int GetRoomsCount()
    {
        return PhotonNetwork.CountOfRooms;
    }

    // 방 생성
    public void CreateRoom(string _roomName, int _playerCount)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = _playerCount;
        PhotonNetwork.CreateRoom(_roomName, options);
    }

    public bool JoinRoom(string _roomName)
    {
        return PhotonNetwork.JoinRoom(_roomName);
    }

    public List<RoomList> GetRooms()
    {
        List<RoomList> roomLists = new List<RoomList>();
        foreach ( RoomInfo _info in roomList)
        {
            RoomList _room = new RoomList();
            _room.roomName = _info.Name;
            _room.playerNum = _info.PlayerCount;
            _room.maxPlayers = _info.MaxPlayers;
            roomLists.Add(_room);
        }

        return roomLists;
    }

    public void PlayGame(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
    public override void OnRoomListUpdate(List<RoomInfo> _roomList)
    {
        base.OnRoomListUpdate(_roomList);
        roomList = roomList.Union(_roomList).ToList();
        roomList = roomList.Where(room => !room.RemovedFromList).ToList();
    }
}
