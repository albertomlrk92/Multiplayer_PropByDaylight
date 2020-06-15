using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetwork : NetworkManager
{
    public GameObject propPrefab;
    public GameObject hunterPrefab;
    int count = 0;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
       if(numPlayers<2)
        {
            playerPrefab = hunterPrefab;
        }else
        {
            playerPrefab = propPrefab;
        }
    }


}
