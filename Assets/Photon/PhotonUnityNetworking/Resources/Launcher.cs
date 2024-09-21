using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class LAUNCHER : MonoBehaviourPunCallbacks
{
    public static LAUNCHER instance;
    public TMP_Text loadingText;
    public GameObject menuButton;
    public GameObject CreateRoomScreen;
    public TMP_InputField roomNameInput; 


    public GameObject loadingScreen;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        CloseAllMenu();
        loadingScreen.SetActive(true);
        loadingText.text = "Connecting to Server...";
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CloseAllMenu()
    {
        menuButton.SetActive(false);
        loadingScreen.SetActive(false);
        CreateRoomScreen.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        loadingText.text = "Joining lobby";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        loadingScreen.SetActive(false);
    }
    public void OpenRoomCreate()
    {
        CloseAllMenu(); 
        CreateRoomScreen.SetActive(true);
    }
}
