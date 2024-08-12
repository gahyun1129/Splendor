using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // 포톤 클라우드 접속
        PhotonNetwork.ConnectUsingSettings();
    }

}
