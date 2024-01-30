using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _createRoomName;
    [SerializeField] private TMP_InputField _joinRoomName;

    [SerializeField] private string _levelName;

    public void CreateRoom() => PhotonNetwork.CreateRoom(_createRoomName.text);
    public void JoinRoom() => PhotonNetwork.JoinRoom(_joinRoomName.text);
    public override void OnJoinedRoom() => PhotonNetwork.LoadLevel(_levelName);
}
