using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemInteract : MonoBehaviour
{
    public bool canTotem = false;
    public GameObject currentTotem;
    public LoadTotem myLoadingBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totemConfig();
    }
    private void totemConfig()
    {
        if (canTotem && Input.GetKeyDown(KeyCode.E))
        {
            myLoadingBar.activatingeTotem();
            myLoadingBar.currentTotem = currentTotem;
        }
        else if (myLoadingBar.enabled && !canTotem)
        {
            myLoadingBar.desactivateLoading();
            myLoadingBar.currentTotem = null;
        }
    }
}
