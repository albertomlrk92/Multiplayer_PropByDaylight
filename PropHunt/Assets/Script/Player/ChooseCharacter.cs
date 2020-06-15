using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacter : MonoBehaviour
{
    public GameObject prop;
    public GameObject hunter;

    public Camera scene;


    [SerializeField]
    Behaviour[] hunterComponents;
    [SerializeField]
    Behaviour[] propComponents;
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

    void EnableComponentsHunter()
    {
        for (int i = 0; i < hunterComponents.Length; i++)
        {
            hunterComponents[i].enabled = true;
        }
    }

    void EnableComponentsProp()
    {
        for (int i = 0; i < propComponents.Length; i++)
        {
            propComponents[i].enabled = true;
        }
    }
    void SceneCameraDisable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(hunter.activeSelf)
        {
            EnableComponentsHunter();
        }
        if (prop.activeSelf)
        {
            EnableComponentsProp();
        }
    }
}
