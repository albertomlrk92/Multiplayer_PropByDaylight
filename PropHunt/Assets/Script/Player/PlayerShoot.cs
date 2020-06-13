using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{

    [SerializeField]
    private PlayerWeapon weapon;
    

    [SerializeField]
    private Camera cam;
    public LayerMask mask;

    public GameObject muzzle;
    //public AudioSource shootEffect;
    //public AudioClip pewpew;

    // Start is called before the first frame update
    void Start()
    {
        if(cam == null)
        {
            Debug.Log("No camera reference");
            this.enabled = false;
        }
        //shootEffect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(weapon.fireRate <= 0f)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Shoot();
                muzzle.GetComponent<ParticleSystem>().Play();
            }
        }
        else
        {
            if(Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f/weapon.fireRate); 
            }
            else if(Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }

        }
        
    }

    [Command]
    void CmdOnShoot()
    {
        RpcDoShootEffect();
    }

    //Is called on all clients when we need to to
    // a shoot effect
    [ClientRpc]
    void RpcDoShootEffect()
    {
        muzzle.GetComponent<ParticleSystem>().Play();
        //shootEffect.PlayOneShot(pewpew, 0.5f);
        

    }

    [Client]
    private void Shoot()
    {
        Debug.Log("Test shoot");
        CmdOnShoot();
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hit, weapon.range, mask))
        {
            Debug.Log("We hit" + hit.collider.name);
            if (hit.collider.tag == "Player")
            {
                CmdPlayerShot(hit.collider.name, weapon.damage);
            }
        }

    }

    [Command]
    void CmdPlayerShot(string playerID, int damage)
    {
        Debug.Log(playerID + " has been shot,");
        PlayerManager player = GameManager.GetPlayer(playerID);
        player.RpcTakeDamage(damage);
        //Destroy(GameObject.Find(ID));
    }
}
