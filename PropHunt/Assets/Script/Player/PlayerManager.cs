using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerSetupNet))]
public class PlayerManager : NetworkBehaviour
{
    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    [SerializeField]
    private GameObject[] disableGameObjectsOnDeath;

    private bool firstSetup = true;

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;
    

    public float GetHealthPct()
        {
            return (float) currentHealth / maxHealth;
        }


    public void SetupPlayer()
    {
        if (isLocalPlayer)
        {
            //Switch cameras
            GameManager.instance.setSceneCameraActive(false);
            GetComponent<PlayerSetupNet>().playerUIInstance.SetActive(true);
        }

        CmdBroadCastNewPlayerSetup();
    }

    [Command]
    private void CmdBroadCastNewPlayerSetup()
    {
        RpcSetupPlayerOnAllClients();
    }

    [ClientRpc]
    private void RpcSetupPlayerOnAllClients()
    {
        if (firstSetup)
        {
            wasEnabled = new bool[disableOnDeath.Length];
            for (int i = 0; i < wasEnabled.Length; i++)
            {
                wasEnabled[i] = disableOnDeath[i].enabled;
            }

            firstSetup = false;
        }

        SetDefaults();
    }

    //void Update()
    //{
    //    if (!isLocalPlayer)
    //        return;

    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        RpcTakeDamage(500);
    //    }
    //}

    [ClientRpc]
    public void RpcTakeDamage(int _amount)
    {
        if (isDead) return;

        currentHealth -= _amount;

        Debug.Log(transform.name + "now has" + currentHealth + "health");
        if(currentHealth <= 0)
        {
            Die();
        }

    }
    
    private void Die()
    {
        isDead = true;

        //Disable components

        for(int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(false);
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
        {
            _col.enabled = false;
        }

        if(isLocalPlayer)
        {
            GameManager.instance.setSceneCameraActive(true);
            GetComponent<PlayerSetupNet>().playerUIInstance.SetActive(false);
        }

        Debug.Log(transform.name + " is deadd");

        //RESPAWN method, it should be called at the beggining on every round

        StartCoroutine(Respawn());

    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        
        Transform _startPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _startPoint.position;
        transform.rotation = _startPoint.rotation;

        yield return new WaitForSeconds(0.1f);
        SetupPlayer();

        Debug.Log(transform.name + " respawned");
    }
    public void SetDefaults()
    {
        isDead = false;

        currentHealth = maxHealth;

        //Enable the components
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        //EnableGameObjects
        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(true);
        }

        //Enable Colliders
        Collider _col = GetComponent<Collider>();
        if(_col != null)
        {
            _col.enabled = true;
        }

    }
}
