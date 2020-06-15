using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacter : MonoBehaviour
{
    public GameObject prop;
    public GameObject hunter;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("Hunter")!=null)
        {
            prop.SetActive(true);
            prop.transform.root.name = "Prop";
        }
        else
        {
            hunter.SetActive(true);
            hunter.transform.root.name = "Hunter";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
