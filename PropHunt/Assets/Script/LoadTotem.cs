using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTotem : MonoBehaviour
{
    public Image loadingBar;
    public Text textIndicator;
    public Transform textLoading;

    public float currentAmount;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            if (currentAmount < 100)
            {
                currentAmount += speed * Time.deltaTime;
                textIndicator.text = ((int)currentAmount).ToString() + "%";
                textLoading.gameObject.SetActive(true);
            }
            else
            {
                textLoading.gameObject.SetActive(false);
                //textIndicator.GetComponent<Text>().text = "DONE";
            }
            loadingBar.fillAmount = currentAmount / 100;

        }
        else if(currentAmount==100)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void activateTotem()
    {
        if (!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);
    }
    public void desactivateLoading()
    {
        if(this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }

}
