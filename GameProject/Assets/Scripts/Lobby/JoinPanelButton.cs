using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoinPanelButton : MonoBehaviour
{
    public Text roomsNum;
    public float space = 10f;
    public ScrollRect scrollRect;
    public GameObject room;
    public List<RectTransform> uiObjects = new List<RectTransform>();

    public void ClickCancle()
    {
        gameObject.SetActive(false);
    }

    public void ClickJoin()
    {
        NetworkManager.instance.PlayGame("GameScene");
    }

    public void ClickRoomButton()
    {

    }

    public void UpdateLobby()
    {
        foreach (RectTransform t in uiObjects)
        {
            Destroy(t.gameObject);
        }
        uiObjects.Clear();

        List<RoomList> list = NetworkManager.instance.GetRooms();
        roomsNum.text = list.Count.ToString();

        foreach (RoomList _room in list)
        {
            var newUi = Instantiate(room, scrollRect.content).GetComponent<RectTransform>();
            newUi.GetChild(0).GetComponent<Text>().text = _room.roomName;
            uiObjects.Add(newUi);
        }

        // 객체의 y값 설정
        float y = 0f;
        for (int i = 0; i < uiObjects.Count; i++)
        {
            uiObjects[i].anchoredPosition = new Vector2(0f, -y);
            y += uiObjects[i].sizeDelta.y + space;
        }
        // content 영역 설정
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, y);
    }
}
