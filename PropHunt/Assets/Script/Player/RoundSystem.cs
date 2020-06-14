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
    private int randomHunter;

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
        randomHunter = Random.Range(1, networkManager.numPlayers);
        StartCoroutine(RoundTimer());
         
        //networkManager.spawnPrefabs. ???

        //scheme: wait time --> pre round --> round--> finish round-->Restart
        
        //if wait time hits 0 or numPlayers == roomSize
        //SelectHunter();
    }

    private void SelectHunter()
    {
        //turn it in hunter
        //currentPlayers[randomHunter-1].SetActive(false); //funciona
        
        //disable prop components and enable hunter ones?
    }

    private void Update()
    {
        currentPlayers = GameObject.FindGameObjectsWithTag("Player");
        //CURRENT AMOUNT OF PLAYERS
        Debug.Log(" Numero de players a int  " + networkManager.numPlayers +
                    "numero de tags con player: " + currentPlayers.Length);

        if(preround ==false)
        {
            roundTime -= Time.deltaTime;
            //Debug.Log(roundTime);
        }
        else
        {
            SelectHunter();
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

        //Check if round timer hits 0
        if(roundTime <= 0f && !victoryProps)
        {
            victoryHunter = true;
        }
        //victory, then reload scene and start new round
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
