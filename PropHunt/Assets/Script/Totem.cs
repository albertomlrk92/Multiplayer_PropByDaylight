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
        if(active)
        {
            Debug.Log("Totem active");
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Movment1>().canTotem = true;
            other.GetComponent<Movment1>().currentTotem = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Movment1>().canTotem = false;
            other.GetComponent<Movment1>().currentTotem = null;
        }
    }
}
