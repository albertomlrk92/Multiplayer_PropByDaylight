using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PropTransform : NetworkBehaviour
{
    // Start is called before the first frame update
    public float distanceOfTransform;
    public Camera myCamera;
    public GameObject[] prefabs;
    public Behaviour[] propScripts;
    public GameObject actualPrefab;
    public Behaviour currentMov;

    private void Start()
    {
        currentMov.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }
            

        if (Input.GetMouseButtonDown(0))
        {
            if (isServer)
            {
                RpcSwapProp();
            }
            else
            {
                CmdSwapProp();
            }
        }
    
    }
    private GameObject GetPrefab(string name)
    {
        bool exist = false;
        foreach(GameObject go in prefabs)
        {
           exist = go.CompareTag(name);
            if(exist)
            {
                Debug.Log("Tag " + name + " is in the list");
                return go;
            }
        }
        Debug.Log("Tag " + name + " is not in the list");
        return null;
    }

    private void propTransform()
    {
        Ray ray = myCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit = new RaycastHit();
        Debug.DrawRay(ray.origin, ray.direction * distanceOfTransform, Color.red, 2.0f);
        if (Physics.Raycast(ray, out hit, distanceOfTransform))
        {
            Debug.Log("I'm looking at " + hit.transform.name);
            GameObject prefab = GetPrefab(hit.transform.tag);
            if (prefab != null)
            {
                actualPrefab.SetActive(false);

                actualPrefab = prefab;

                actualPrefab.SetActive(true);       
                
                changeMov();

            }

        }
    }
    private void changeMov()
    {
       // actualPrefab.GetComponent<SphereCollider>().enabled = false;
        switch (actualPrefab.tag)
        {
            case "Ghost":
                currentMov.enabled = false;
                currentMov = propScripts[0];
                currentMov.enabled = true;
                break;
            case "PaperPlane":
                currentMov.enabled = false;
                currentMov = propScripts[1];
                currentMov.enabled = true;
                break;
            case "Fridge":
                currentMov.enabled = false;
                currentMov = propScripts[2];
                currentMov.enabled = true;
                currentMov.GetComponent<SimpleMasMov>().changeRbattributes(actualPrefab.tag);
                break;

        }
    }
    [Command]
    void CmdSwapProp()
    {
        //Apply it to all other clients
        RpcSwapProp();
    }

    [ClientRpc]
    void RpcSwapProp()
    {
        propTransform();
    }
}
