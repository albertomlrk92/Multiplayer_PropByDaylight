using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTotem : MonoBehaviour
{
    public Image loadingBar;
    public Text textIndicator;
    public Transform textLoading;

    public float speed;
    public GameObject currentTotem;
    private float currentAmount;
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
            Debug.Log(currentTotem.GetComponent<Totem>().actualcharge);
            currentAmount = currentTotem.GetComponent<Totem>().actualcharge;
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
            currentTotem.GetComponent<Totem>().actualcharge = currentAmount;
            loadingBar.fillAmount = currentTotem.GetComponent<Totem>().actualcharge/100;
            if (currentAmount >= 100)
            {
                currentTotem.GetComponent<Totem>().active = true;
                Invoke("desactivate", 1.0f);
            }

        }
    }
    public void activatingeTotem()
    {
        if (!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);
    }
    public void desactivateLoading()
    {
        if(this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }
    private void desactivate()
    {
        this.gameObject.SetActive(false);
    }

}
