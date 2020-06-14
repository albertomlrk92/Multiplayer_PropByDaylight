using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    // Start is called before the first frame update
    public bool active;
    public float actualcharge;
    void Start()
    {
        active = false;
        actualcharge = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.tag == "Player")
        {
            Debug.Log("Enter");
            other.GetComponent<TotemInteract>().canTotem = true;
            other.GetComponent<TotemInteract>().currentTotem = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<TotemInteract>().canTotem = false;
            other.GetComponent<TotemInteract>().currentTotem = null;
        }
    }
}
