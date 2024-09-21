using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{

    public static Launcher instance;


    public TMP_Text loadingText;
    public GameObject menuButton;

    public GameObject loadingScreen;

    public GameObject createRoomScreen;
    public TMP_InputField roomNameInput;


    public GameObject roomScreen;
    public TMP_Text roomNameText;

    public GameObject errorScreen;
    public TMP_Text errorText;

    public GameObject roomBrowserScreen;
    public RoomButton roomButton;
    private List<RoomButton> allRoomButtons = new List<RoomButton>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        CloseAllMenus();
        loadingScreen.SetActive(true);
        loadingText.text = "Connecting to server";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        loadingScreen.SetActive(false);
        menuButton.SetActive(true);
        PhotonNetwork.JoinLobby();
        loadingText.text = "Joining Lobby...";
    }

    public override void OnJoinedLobby()
    {
        CloseAllMenus();
        menuButton.SetActive(true);
    }

    public void CloseAllMenus()
    {
        menuButton.SetActive(false);
        loadingScreen.SetActive(false);
        createRoomScreen.SetActive(false);
        roomBrowserScreen.SetActive(false);
    }

    public void OpenRoomCreate()
    {
        CloseAllMenus();
        createRoomScreen.SetActive(true);
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInput.text))
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 8;

            PhotonNetwork.CreateRoom(roomNameInput.text, options);

            CloseAllMenus();
            loadingText.text = "Creating Room....";
            loadingScreen.SetActive(true);
        }
    }

    public void OpenRoomBrowser()
    {
        CloseAllMenus();
        roomBrowserScreen.SetActive(true);
    }

    public void CloseRoomBrowser()
    {
        CloseAllMenus();
        menuButton.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomButton rb in allRoomButtons)
        {
            Destroy(rb.gameObject);
        }
        allRoomButtons.Clear();

        roomButton.gameObject.SetActive(false);

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].PlayerCount != roomList[i].MaxPlayers && !roomList[i].RemovedFromList)
            {
                RoomButton newButton = Instantiate(roomButton, roomButton.transform.parent);
                newButton.SetButtonDetails(roomList[i]);

                roomButton.gameObject.SetActive(true);

                allRoomButtons.Add(newButton);
            }
        }
    }

    public void JoinRoom(RoomInfo inputInfo)
    {
        PhotonNetwork.JoinRoom(inputInfo.Name);

        CloseAllMenus();
        loadingText.text = "Joining Room";
        loadingScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
