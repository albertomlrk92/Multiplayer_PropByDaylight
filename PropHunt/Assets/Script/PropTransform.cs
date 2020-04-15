using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropTransform : MonoBehaviour
{
    // Start is called before the first frame update
    public float distanceOfTransform;
    public Camera myCamera;
    public GameObject[] prefabs;
    public GameObject actualPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = myCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit = new RaycastHit();
        if (Input.GetMouseButtonDown(0))
        {
            CmdTransform(ray, hit);
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


    //[Command]
    private void CmdTransform(Ray _ray, RaycastHit _hit)
    {
        Debug.DrawRay(_ray.origin, _ray.direction * distanceOfTransform, Color.red, 2.0f);
        if (Physics.Raycast(_ray, out _hit, distanceOfTransform))
        {
            Debug.Log("I'm looking at " + _hit.transform.name);
            GameObject prefab = GetPrefab(_hit.transform.tag);
            if (prefab != null)
            {
                RpcChangePrefab(prefab);
            }

        }
    }
    //[ClientRpc]
    private void RpcChangePrefab(GameObject _prefab)
    {
        GameObject clone = (GameObject)Instantiate(_prefab, transform.position, transform.rotation);
        clone.transform.parent = transform;
        Destroy(actualPrefab);
        actualPrefab = clone;
    }
}
