using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class RPCTest : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsServer && IsOwner)
        {
            
        }
    }
}
