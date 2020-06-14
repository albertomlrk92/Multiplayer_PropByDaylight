using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundSystem : NetworkBehaviour
{

    public static RoundSystem instance;
    private GameObject[] currentPlayers;
    private PlayerManager[] players;

    private GameObject[] totemsInScene;
    private int totemsToWin = 3;
    private int numberOfTotemsActive = 0;

    private NetworkManager networkManager;

    private bool victoryHunter = false;
    private bool victoryProps = false;
    private bool doorsOpen = false;
    private int randomHunter;

    [SerializeField]
    private GameObject hunterPrefab;
    [SerializeField]
    private GameObject propPrefab;

    public GameObject playerUI;
    public Text victoryTextUI;

    bool preround = true;
    bool inPreparationTime = true;
    private float roundTime = 15f;


    private void Awake()
    {

        if (instance != null)
        {
            Debug.Log("More than one round system on scene");
        }
        else
        {
            instance = this;
        }

        totemsInScene = GameObject.FindGameObjectsWithTag("Totem");
        
    }

    private void Start()
    {
        networkManager = NetworkManager.singleton;
        randomHunter = Random.Range(1, networkManager.numPlayers);

        if (networkManager.numPlayers <= 1)
        {
            networkManager.playerPrefab = hunterPrefab;
        }
        StartCoroutine(RoundTimer());

        //networkManager.playerPrefab.

        //scheme: wait time --> pre round --> round--> finish round-->Restart
        
        //if wait time hits 0 or numPlayers == roomSize
        //SelectHunter();
    }

    private void SelectHunter()
    {
        if(networkManager.numPlayers > 1)
        {
            networkManager.playerPrefab = propPrefab;
            Debug.Log("Aqui el last player seria PROP");
        }
        //disable prop components and enable hunter ones?
    }

    
    private void Update()
    {
        //When the first player enters, he will be Hunter,then, the next ones will be props
        SelectHunter();

        

        currentPlayers = GameObject.FindGameObjectsWithTag("Player");
        //CURRENT AMOUNT OF PLAYERS
        //Debug.Log(" Numero de players a int  " + networkManager.numPlayers +"numero de tags con player: " + currentPlayers.Length);

        //Pre-Round
        if(preround ==false)
        {
            Debug.Log("Pre-Round End");

            if(inPreparationTime==false)
            {
                Debug.Log("RoundTimeCounting");
                roundTime -= Time.deltaTime;
            }
           
            //Debug.Log(roundTime);
        }
        else
        {
            
        }

        CheckTotemsActive();
        CheckIfAllDead();

        if(numberOfTotemsActive >= totemsToWin)
        {
            Debug.Log("Doors Open!");
        }
        
    }


    public void CheckTotemsActive()
    {
        foreach (GameObject go in totemsInScene) //this only works if all totems are active, so the doors will open then.
        {
            if (go.GetComponent<Totem>().active == true)
            {
                doorsOpen = true;
                Debug.Log("DOORS OPEN!");
            }
        }
        //for (int i= 0; i<totemsInScene.Length;i++)
        //{

        //    if(totemsInScene[i].GetComponent<Totem>().active)
        //    {
        //        numberOfTotemsActive++;
        //    }
        //}
    }
    public void CheckIfAllDead()
    {
        //Check if Props are dead
        foreach (GameObject go in currentPlayers)
        {
            if (go.GetComponent<PlayerManager>().isDead != true)
            {
                victoryHunter = false;
                Debug.Log("Props alive");   
            }
            else
            {
                Debug.Log("All props are dead");
                victoryHunter = true;
            }

        }
        //Text victoryTextUI = playerUI.GetComponent<Text>();

        if(!victoryHunter && !victoryProps)
        {
           victoryTextUI.text = "";
        }
            


        //Check if round timer hits 0
        if (roundTime <= 0f && !victoryProps)
        {
            victoryHunter = true;
        }


        //victory, then reload scene and start new round
        if (victoryHunter == true)
        {

            Debug.Log("HUNTER WON!");
            //victoryText.
            victoryTextUI.text = "Hunter Won!";
            networkManager.ServerChangeScene("EscenaEdu");
        }
        if (victoryProps == true)
        {
            Debug.Log("PROPS WON!");
            victoryTextUI.text = "Props Won!";
        }
            
    }

    IEnumerator RoundTimer()
    {
        Debug.Log("Pre-Round start");
        yield return new WaitForSeconds(10.0f);
        preround = false;
        StartCoroutine(Wait10Seconds());
        
    }

    IEnumerator Wait10Seconds()
    {
        Debug.Log("10 Seconds for hiding");
        yield return new WaitForSeconds(10.0f);
        inPreparationTime = false;

    }

    IEnumerator RestartRound()
    {
        yield return new WaitForSeconds(10.0f);
        //networkManager.ServerChangeScene("EscenaEdu");
        //networkManager.StopServer();
    }

    

}
