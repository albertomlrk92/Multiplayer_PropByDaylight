using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotorController))]
[RequireComponent(typeof(ConfigurableJoint))]
public class ControllerPlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speedPlayer = 5f;

    [SerializeField]
    private float lookSensivility = 3f;

    [SerializeField]
    private float jumpForce = 1000f;

    [Header("Spring Options")]
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;



    private ConfigurableJoint joint;
    private PlayerMotorController motor;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotorController>();
        joint = GetComponent<ConfigurableJoint>();
        SetJointSettings(jointSpring);
    }

    // Update is called once per frame
    void Update()
    {

        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        //final movement vector
        Vector3 velocity = (movHorizontal + movVertical) * speedPlayer;

        //apply movement
        motor.Move(velocity);

        //Rotation HORIZONTAL, we will turn the player to look around him.
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotationPlayer = new Vector3(0f, yRot, 0f) * lookSensivility;

        //Apply
        motor.Rotate(rotationPlayer);

        //Rotation VERTICAL, we will turn the camera in a vertical axis, why? We dont wanna turn the player vertically only camera.
        float xRot = Input.GetAxisRaw("Mouse Y");
        float cameraRotation = xRot * lookSensivility;

        //Apply 
        motor.RotateCamera(cameraRotation);


        //Calculate Jump
        Vector3 jump = Vector3.zero;
        //Apply jump Force
        if(Input.GetButton("Jump"))
        {
            jump = Vector3.up * jumpForce;
            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(jointSpring);

        }

        motor.ApplyJump(jump);


    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive
        {
            positionSpring = jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
