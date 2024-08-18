using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoinPanelButton : MonoBehaviour
{
    public Text roomsNum;
    public Transform content;
    public GameObject room;
    public GameObject lobby;
    private bool inLobby;
    private float currentTime = 0f;
    private float updateDelay = 2f;

    private void Start()
    {
        StartCoroutine(JoinLobby());
    }
    public void ClickCancle()
    {
        inLobby = false;
        gameObject.SetActive(false);
    }

    public void ClickJoin()
    {
       
    }

    public void ClickRoom()
    {
        GameObject _room = EventSystem.current.currentSelectedGameObject;
        string roomName = _room.transform.GetChild(0).GetComponent<Text>().text;
        string[] roomCount = _room.transform.GetChild(1).GetComponent<Text>().text.Split('/');
        string _playerCount = roomCount[0].Trim();
        string _maxPlayers = roomCount[1].Trim();
        int playerCount = int.Parse(_playerCount);
        int maxPlayers = int.Parse(_maxPlayers);

        if (playerCount < maxPlayers)
        {
            bool joined = NetworkManager.instance.JoinRoom(roomName);   
            if (joined)
            {
                // 게임 시작
            }
        }
    }
    private void UpdateLobby()
    {
        foreach (Transform _room in content)
        {
            Destroy(_room.gameObject);
        }
        List<RoomList> roomLists = NetworkManager.instance.GetRoomList();
        roomsNum.text = roomLists.Count.ToString();

        foreach (RoomList roomList in roomLists)
        {
            GameObject tmp = Instantiate(room, content);
            tmp.transform.GetChild(0).GetComponent<Text>().text = roomList.roomName;
            string count = string.Format("{0} / {1}", roomList.playerNum, roomList.maxPlayers);
            tmp.transform.GetChild(1).GetComponent <Text>().text = count;
            tmp.GetComponent<Button>().onClick.AddListener(delegate { ClickRoom(); });
        }
    }
    private void Update()
    {
        if ( inLobby)
        {
            currentTime += Time.deltaTime;
            if ( currentTime > updateDelay )
            {
                UpdateLobby();
                currentTime = 0f;
            }
        }
        roomsNum.text = NetworkManager.instance.GetRoomsCount().ToString();
    }

    private IEnumerator JoinLobby()
    {
        yield return StartCoroutine(NetworkManager.instance.JoinGame());
        inLobby = NetworkManager.instance.CheckInLobby();
    }
}
