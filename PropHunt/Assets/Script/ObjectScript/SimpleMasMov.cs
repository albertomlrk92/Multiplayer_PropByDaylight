using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMasMov : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed = 20.0f;
    public float acceleration = 20.0f;
    public float speed = 20.0f;
    public float jumpSpeed = 8.0f;


    private float inputX;
    private float inputZ;

    public bool canTotem = false;
    public GameObject currentTotem;
    public LoadTotem myLoadingBar;

    private Vector3 vel = new Vector3(0.0f, 0.0f, 0.0f);

    private Rigidbody rb;
    private float mass;
    private PlayerMotorController motor;
    void Start()
    {
        motor = GetComponent<PlayerMotorController>();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");
        //Movement with acceleration  
        Vector3 targetSpeed = new Vector3(speed * inputX, 0, speed * inputZ);   //speed * inputs.y
        Vector3 velOffset = targetSpeed - vel;
        float maxOffset = acceleration * dt;
        velOffset = Vector3.ClampMagnitude(velOffset, maxOffset);
        vel += velOffset;
        transform.position += vel * dt;

    }
    public void changeRbattributes(string prefab)
    {
        switch (prefab)
        {
            case "Fridge":
                mass = 150;
                break;
            case "Botella":
                break;
            case "Taburete":
                break;
            case "Sofa":
                break;
            case "Silla":
                break;
            case "Mesa":
                break;
            case "Maceta":
                break;
            case "Lata":
                break;
            case "Lampara":
                break;
            case "Jarra":
                break;
            case "Caja":
                break;
        }
        rbApplication();
    }
    private void rbApplication()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        rb.useGravity = true;
    }
}
