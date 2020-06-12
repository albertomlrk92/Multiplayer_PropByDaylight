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
    public float fridgeMass;

    private float inputX;
    private float inputZ;

    public bool canTotem = false;
    public GameObject currentTotem;
    public LoadTotem myLoadingBar;

    private Vector3 vel = new Vector3(0.0f, 0.0f, 0.0f);

    private Rigidbody rb;
    private float mass;
    private PlayerMotorController motor;
    private float lookSensivility = 3f;
    void Start()
    {
        motor = GetComponent<PlayerMotorController>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        //final movement vector
        Vector3 velocity = (movHorizontal + movVertical) * speed/rb.mass;

        //apply movement
        motor.Move(velocity);
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotationPlayer = new Vector3(0f, yRot, 0f) * lookSensivility;

        //Apply
        motor.Rotate(rotationPlayer);

        //Rotation VERTICAL, we will turn the camera in a vertical axis, why? We dont wanna turn the player vertically only camera.
        float xRot = Input.GetAxisRaw("Mouse Y");
        float cameraRotation = xRot * lookSensivility;

        //Apply 
        motor.RotateCamera(cameraRotation);

    }
    public void changeRbattributes(string prefab)
    {
        switch (prefab)
        {
            case "Fridge":
                mass = fridgeMass;
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
        rb.mass = mass;
        Debug.Log(rb.mass);
        rb.useGravity = true;
    }
    private void OnEnable()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
