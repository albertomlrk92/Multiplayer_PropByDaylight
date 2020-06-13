using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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


    bool preround = true;
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
        StartCoroutine(RoundTimer());
        
    }
    private void Update()
    {
        currentPlayers = GameObject.FindGameObjectsWithTag("Player");
        //players = gameObject.GetComponents<PlayerManager>();
        //CURRENT AMOUNT OF PLAUYERS
        Debug.Log(" Numero de players en la escena int  " + networkManager.numPlayers +
                    "numero de tags con player: " + currentPlayers.Length);
        //for(int i =0; i < currentPlayers.Length; i++ )
        //{
        //    if(currentPlayers[i].GetComponent<PlayerManager>().isDead)
        //    {

        //    }
        //}


        if(preround ==false)
        {
            roundTime -= Time.deltaTime;
            Debug.Log(roundTime);
        }

        CheckTotemsActive();
        CheckIfAllDead();

        if(numberOfTotemsActive >= totemsToWin)
        {
            Debug.Log("Doors Open!");
        }
        else
        {
            Debug.Log("Doors Closed!");
        }
        
    }
    public void CheckTotemsActive()
    {
        //foreach (GameObject go in totemsInScene)
        //{
        //    if (go.GetComponent<Totem>().active != true)
        //    {
        //        doorsOpen = true;
        //        Debug.Log("DOORS OPEN!");
        //    }
        //}

        for(int i= 0; i<totemsInScene.Length;i++)
        {
            if(totemsInScene[i].GetComponent<Totem>().active)
            {
                numberOfTotemsActive++;
            }
        }
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
        if(roundTime <= 0f && !victoryProps)
        {
            victoryHunter = true;
        }
        if (victoryHunter == true) 
            Debug.Log("HUNTER WON!");

        if (victoryProps == true) 
            Debug.Log("PROPS WON!");
    }

    IEnumerator RoundTimer()
    {
        
        yield return new WaitForSeconds(5.0f);
        preround = false;
        
    }


}
