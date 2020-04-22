using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayerMovement : MonoBehaviour

    
{
    [SerializeField]
    private float speedPlayer = 5f;

    [SerializeField]
    private float lookSensivility = 3f;


    private PlayerMotorController motor;
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotorController>();
    }

    // Update is called once per frame
    void Update()
    {

        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        //final movement vector
        Vector3 velocity = (movHorizontal + movVertical).normalized * speedPlayer;

        //apply movement
        motor.Move(velocity);

        //Rotation HORIZONTAL, we will turn the player to look around him.
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotationPlayer = new Vector3(0f, yRot, 0f) * lookSensivility;

        //Apply
        motor.Rotate(rotationPlayer);

        //Rotation VERTICAL, we will turn the camera in a vertical axis, why? We dont wanna turn the player vertically only camera.
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensivility;

        //Apply 
        motor.RotateCamera(cameraRotation);


    }
}
