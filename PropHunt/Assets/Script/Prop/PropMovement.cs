using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PropMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 20.0f;
    public float jumpSpeed = 8.0f;


    private Vector3 vel = new Vector3(0.0f, 0.0f, 0.0f);
    private float lookSensivility = 3f;
    private PlayerMotorController motor;
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
        Vector3 velocity = (movHorizontal + movVertical) * speed;

        //apply movement
        motor.Move(velocity);
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotationPlayer = new Vector3(0f, yRot, 0f) * lookSensivility;

        //Apply
        motor.Rotate(rotationPlayer);

        //Rotation VERTICAL, we will turn the camera in a vertical axis, why? We dont wanna turn the player vertically only camera.
        float xRot = Input.GetAxisRaw("Mouse Y");
        float cameraRotation = xRot * lookSensivility;
        Debug.Log(cameraRotation);

        //Apply 
        motor.RotateCamera(cameraRotation);

    }


}

