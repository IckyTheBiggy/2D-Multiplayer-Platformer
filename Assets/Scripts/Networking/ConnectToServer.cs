using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    void Start() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        {
            PhotonNetwork.SendRate = 10;
            PhotonNetwork.SerializationRate = 10;
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby() => SceneManager.LoadScene("Menu");
}