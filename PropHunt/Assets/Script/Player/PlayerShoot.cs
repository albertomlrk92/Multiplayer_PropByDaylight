using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{

    
    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;
    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        if(cam == null)
        {
            Debug.Log("No camera reference");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    private void Shoot()
    {
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
