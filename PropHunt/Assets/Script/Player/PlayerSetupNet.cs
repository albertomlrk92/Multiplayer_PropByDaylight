using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(ControllerPlayerMovement))]

public class PlayerSetupNet : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    string dontDrawLayerName = "DontDraw";

    [SerializeField]
    GameObject playerGraphics;
    [SerializeField]
    GameObject playerUIprefab;

    [HideInInspector]
    public GameObject playerUIInstance;

    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {

            //Disable player graphics for local player
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));

            //Create PlayerUI
            playerUIInstance = Instantiate(playerUIprefab);
            playerUIInstance.name = playerUIprefab.name;

            //Config PlayerUI
            PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();

            if (ui == null)
                Debug.Log("No playerUI component");

            ui.SetPlayer(GetComponent<PlayerManager>());

            GetComponent<PlayerManager>().SetupPlayer();
        }

        
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        
        foreach(Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        PlayerManager player = GetComponent<PlayerManager>();

        GameManager.RegisterPlayer(netID, player);
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }
    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }
    void OnDisable()
    {

        Destroy(playerUIInstance);
        if(isLocalPlayer)
            GameManager.instance.setSceneCameraActive(true);

        GameManager.UnregisterPlayer(transform.name);
    }
}
