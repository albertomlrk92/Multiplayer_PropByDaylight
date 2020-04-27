using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour
{
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
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;


    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for(int i=0;i<wasEnabled.Length;i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();
    }

    //void Update()
    //{
    //    if (!isLocalPlayer)
    //        return;

    //    if(Input.GetKeyDown(KeyCode.K))
    //    {
    //        RpcTakeDamage(500);
    //    }
    //}

    [ClientRpc]
    public void RpcTakeDamage(int amount)
    {
        if (isDead) return;
        currentHealth -= amount;

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

        Collider _col = GetComponent<Collider>();
        if (_col != null)
        {
            _col.enabled = false;
        }

        Debug.Log(transform.name + " is deadd");

        //RESPAWN method, it should be called at the beggining on every round


        StartCoroutine(Respawn());

    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        SetDefaults();
        Transform _startPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _startPoint.position;
        transform.rotation = _startPoint.rotation;

        Debug.Log(transform.name + " respawned");
    }
    public void SetDefaults()
    {
        isDead = false;

        currentHealth = maxHealth;

        for (int i = 0; i < wasEnabled.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider _col = GetComponent<Collider>();
        if(_col != null)
        {
            _col.enabled = true;
        }
    }
}
