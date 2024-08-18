using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanelButton : MonoBehaviour
{
    [SerializeField]
    InputField roomName;
    [SerializeField]
    Text maxPlayers;

    public void ClickCancle()
    {
        gameObject.SetActive(false);
    }
    public void ClickDown()
    {
        int num = int.Parse(maxPlayers.text);
        if ( num > 2)
        {
            num -= 1;
        }
        maxPlayers.text = (num).ToString();
    }
    public void ClickUp()
    {
        int num = int.Parse(maxPlayers.text);
        if (num < 4)
        {
            num += 1;
        }
        maxPlayers.text = (num).ToString();
    }
    public void ClickCreate()
    {
        NetworkManager.instance.CreateRoom(roomName.text, int.Parse(maxPlayers.text));
    }
}
