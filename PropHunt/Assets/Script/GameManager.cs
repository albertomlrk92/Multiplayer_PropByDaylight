using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public MatchSettings matchSettings;

    [SerializeField]
    private GameObject sceneCamera;

    private void Awake()
    {

       if(instance != null)
        {
            Debug.Log("More than one game manager on scene");
        }
        else
        {
            instance = this;
        }
    }

    public void setSceneCameraActive(bool isActive)
    {
        if(sceneCamera==null)
        {
            return;
        }

        sceneCamera.SetActive(isActive);
    }

    #region Player Tracking
    private const string PLAYER_ID_PREFIX = "Player "; //id del player
    private static Dictionary<string, PlayerManager> players = new Dictionary<string, PlayerManager>(); //

    public static void RegisterPlayer(string netID, PlayerManager player) //Registrar
    {
        string playerID = PLAYER_ID_PREFIX + netID;
        players.Add(playerID, player);
        player.transform.name = playerID;
    }

    public static void UnregisterPlayer(string playerID) 
    {
        players.Remove(playerID);
    }

    public static PlayerManager GetPlayer(string playerID)
    {
        return players[playerID];
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200,200,200,500));
        GUILayout.BeginVertical();

        foreach (string playerID in players.Keys)
        {
            GUILayout.Label(playerID + " - " + players[playerID].transform.name);
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
    #endregion


}
