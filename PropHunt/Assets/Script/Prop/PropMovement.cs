using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PropMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed = 20.0f;
    public float acceleration = 20.0f;
    public float speed = 20.0f;
    public float jumpSpeed = 8.0f;


    private float inputX;
    private float inputY;
    private float inputZ;

    public bool canTotem = false;
    public GameObject currentTotem;
    public LoadTotem myLoadingBar;

    private Vector3 vel = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");
        inputY = Input.GetAxisRaw("Jump");
        //Movement with acceleration  
        Vector3 targetSpeed = new Vector3(speed * inputX, jumpSpeed * inputY, speed * inputZ);   //speed * inputs.y
        Vector3 velOffset = targetSpeed - vel;
        float maxOffset = acceleration * dt;
        velOffset = Vector3.ClampMagnitude(velOffset, maxOffset);
        vel += velOffset;
        transform.position += vel * dt;
        totemConfig();

    }
    private void totemConfig()
    {
        if (canTotem && Input.GetKeyDown(KeyCode.E))
        {
            myLoadingBar.activatingeTotem();
            myLoadingBar.currentTotem = currentTotem;
        }
        else if (!canTotem)
        {
            myLoadingBar.desactivateLoading();
            myLoadingBar.currentTotem = null;
        }
    }

}

