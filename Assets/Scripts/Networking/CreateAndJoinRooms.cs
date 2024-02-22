/*
using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _createRoomName;
    [SerializeField] private TMP_InputField _joinRoomName;

    [SerializeField] private Button _createRoomButton;
    [SerializeField] private Button _joinRoomButton;
    
    [SerializeField] private string _levelName;
    
    private void Awake()
    {
        _createRoomButton.onClick.AddListener(() =>
            PhotonNetwork.CreateRoom(_createRoomName.text));
        
        _joinRoomButton.onClick.AddListener(() =>
            PhotonNetwork.JoinRoom(_joinRoomName.text));
    }
    
    public override void OnJoinedRoom() => PhotonNetwork.LoadLevel(_levelName);
}
*/
