using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetwork : NetworkManager
{

    List<GameObject> players = new List<GameObject>();

    public GameObject hunterPrefab;
    int count = 0;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if(numPlayers > count)
        {
            playerPrefab = hunterPrefab;
        }
       
       
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        players.Add(player);
        Debug.Log("Se ha añadidio un player " + player + "count: " + count);
        count++;
        //player.GetComponent<Player>().color = Color.red;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }


}
