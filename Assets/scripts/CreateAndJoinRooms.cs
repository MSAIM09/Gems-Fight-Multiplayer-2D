using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField JoinInput;

    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.CreateRoom(createInput.text);
        }
        else
        {
            Debug.LogError("Not connected and ready to create a room.");
        }
    }
    public void JoinRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinRoom(JoinInput.text);
        }
        else
        {
            Debug.LogError("Not connected and ready to join a room.");
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Room creation failed: {message}");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Room joining failed: {message}");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Photon Network is not connected. Returning to loading scene.");
            // Optionally, you could reload the loading scene or handle this scenario as needed
            // SceneManager.LoadScene("LoadingScene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
