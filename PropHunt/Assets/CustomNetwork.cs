using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetwork : NetworkManager
{
    [SerializeField]
    GameObject[] characters;

    private GameObject selectedChar;

    public void SelectCharacter(int i)
    {
        selectedChar = characters[i];
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (selectedChar == null)
            selectedChar = characters[0];
        Debug.LogError("OnServerAddPlayer");
        GameObject p = (GameObject)GameObject.Instantiate(selectedChar, CustomNetwork.singleton.GetStartPosition().position, CustomNetwork.singleton.GetStartPosition().rotation);
        NetworkServer.AddPlayerForConnection(conn, p, playerControllerId);
        //GetComponent<ChoosePlayer>().enabled = false;
    }


}
