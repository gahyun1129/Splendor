using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyButton : MonoBehaviour
{
    [SerializeField]
    GameObject CreatePanel;
    [SerializeField]
    GameObject JoinPanel;

    public Text num;

    public void ClickCreatePanel()
    {
        CreatePanel.SetActive(true);
    }
    public void ClickJoinPanel()
    {
        JoinPanel.SetActive(true);
    }


}
