using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayerMovement : MonoBehaviour

    
{
    [SerializeField]
    private float speedPlayer = 5f;
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
    }
}
